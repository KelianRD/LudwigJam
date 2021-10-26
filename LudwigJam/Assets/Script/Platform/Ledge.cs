using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField] private GameObject snapPoint;
    [SerializeField] private GameObject standPos;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("LedgeGrabCheck")) return;

        PlayerController player = other.transform.parent.GetComponent<PlayerController>();

        if (player != null)
        {
            player.LedgeGrab(snapPoint.transform.position, this);
        }
    }

    public Vector3 GetStandPos() => standPos.transform.position;
}
