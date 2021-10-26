using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingJumpTrigger : MonoBehaviour
{
    private bool hasTriggered;

    public List<Rigidbody> rigidbodies = new List<Rigidbody>();
    public List<GameObject> winds = new List<GameObject>();

    private void Start()
    {
        hasTriggered = false;
        //foreach (GameObject go in winds)
        //{
        //    Wind w = go.GetComponent<Wind>();
        //    w.direction = Vector3.zero;
        //}
    }

    private void ActivateWind()
    {
        foreach (GameObject go in winds)
        {
            Wind w = go.GetComponent<Wind>();
            w.direction = new Vector3(0, 1, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hasTriggered)
            {
                if (rigidbodies.Count != 0)
                {
                    foreach (Rigidbody rb in rigidbodies)
                    {
                        rb.mass = 45;
                        rb.useGravity = true;
                        rb.constraints = RigidbodyConstraints.None;
                    }

                    //foreach (GameObject go in winds)
                    //{
                    //    go.SetActive(true);
                    //}
                    //Invoke("ActivateWind", 1.5f);
                }
            }
        }
    }
}
