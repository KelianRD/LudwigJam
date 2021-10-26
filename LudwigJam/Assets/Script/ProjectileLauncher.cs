using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePosition;
    public Vector3 directionOverride = new Vector3(0, 1, 0);

    public bool inPlayerFace = true;

    public float projectileSpeed;
    private Vector3 projectileDirection;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (inPlayerFace)
                projectileDirection = (other.transform.position - projectilePosition.position + new Vector3(0, 2, 0)).normalized;
            else
                projectileDirection = directionOverride;

            Instantiate(projectile, projectilePosition.position, Quaternion.identity, transform).GetComponent<Projectile>().velocity = projectileDirection * projectileSpeed;
        }
        
    }
}
