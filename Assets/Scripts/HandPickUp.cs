using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPickUp : MonoBehaviour
{
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private KeyCode keyCode;

    private Collider[] coll;
    private GameObject box = null;

    private void Start()
    {
        coll = new Collider[10];
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (box != null)
            {
                box.transform.parent = null;
                box.GetComponent<Collider>().enabled = true;
                box.GetComponent<Rigidbody>().isKinematic = false;
                box = null;
            }
            else
            {
                coll = Physics.OverlapSphere(transform.position, 0.4f, boxLayer);
                if (coll.Length > 0)
                {
                    foreach (Collider c in coll)
                    {
                        if (c.gameObject.CompareTag("Box") && c.gameObject.transform.parent == null)
                        {
                            box = c.gameObject;
                            c.attachedRigidbody.isKinematic = true;
                            c.enabled = false;
                            box.transform.parent = transform;
                            break;
                        }
                    }
                }
            }
        }
    }
}
