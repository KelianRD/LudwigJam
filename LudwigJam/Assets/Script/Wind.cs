using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    [Tooltip("Direction will be normalized so the value will be 1")]
    public Vector3 direction;

    public float speed = 25;

    private Rigidbody rb;

    private void OnTriggerEnter(Collider other)
    {
        rb = other.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnTriggerExit(Collider other)
    {
        rb.useGravity = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rb.AddForce(direction * speed * Time.deltaTime);
            
        }
    }
}
