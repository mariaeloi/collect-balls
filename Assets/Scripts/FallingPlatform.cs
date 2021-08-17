using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    [SerializeField] private float delayFall = 1f;
    [SerializeField] private float delayReappear = 5f;

    private Rigidbody rigiBody;
    private Vector3 startingPosition;
    private bool isFalling;

    void Start()
    {
        rigiBody = GetComponent<Rigidbody>();
        rigiBody.isKinematic = true;
        startingPosition = transform.position;
        isFalling = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isFalling)
        {
            isFalling = true;
            Invoke("Falling", delayFall);
        }
    }

    private void Falling()
    {
        rigiBody.isKinematic = false;
        Invoke("Reappear", delayReappear);
    }

    private void Reappear()
    {
        isFalling = false;
        rigiBody.isKinematic = true;
        rigiBody.velocity = new Vector3(0f, 0f, 0f);
        transform.position = startingPosition;
    }
}
