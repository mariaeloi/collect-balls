using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ThirdPersonMovement>().Fall();
        }
        else if (other.gameObject.CompareTag("Box")){
            Destroy(other.gameObject);
        }
    }
}
