using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorPlatform : MonoBehaviour
{
    public float speed = 500f;

    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }
}
