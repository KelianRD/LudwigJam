using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform player;
    public LayerMask maskToIgnore;

    public float maxDistance;

    private void Update()
    {
        CheckForObstacle();
    }

    private void CheckForObstacle()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit);

        if (!hit.collider.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Something in front of the player");
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, player.position);
    }
}
