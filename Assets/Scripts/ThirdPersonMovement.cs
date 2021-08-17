using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Vector3 velocity;

    private bool isGrounded;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    private bool active;
    private Vector3 startingPosition;
    private Vector3 bounce = Vector3.zero;

    void Start()
    {
        active = true;
        startingPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (active)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if(bounce != Vector3.zero)
                {
                    moveDirection =  bounce;
                    bounce = Vector3.zero;
                }
                controller.Move(moveDirection * speed * Time.deltaTime);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    public void Fall()
    {
        active = false;
        FindObjectOfType<GameController>().PlayerFall();
    }

    public void Reappear()
    {
        transform.position = startingPosition;
        Invoke("MakeActive", 0.5f);
    }

    private void MakeActive()
    {
        active = true;
    }

    public void SetActive(bool active)
    {
        this.active = active;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Bounce"))
        {
            bounce = hit.normal * 50;
        }
    }
}
