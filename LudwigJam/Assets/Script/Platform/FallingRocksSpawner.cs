using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocksSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public float spawnRate = 2;

    public float xOffset = 0, zOffset = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRock", 0, spawnRate);
    }

    public void SpawnRock()
    {
        Vector3 offset = new Vector3(Random.Range(-xOffset, xOffset), 0, Random.Range(-zOffset, zOffset));

        Instantiate(rockPrefab, transform.position + offset, Quaternion.identity, transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z - zOffset), new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset));
        Gizmos.DrawLine(new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z - zOffset), new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z + zOffset));
        
        Gizmos.DrawLine(new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z - zOffset), new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z - zOffset));
        Gizmos.DrawLine(new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z + zOffset), new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z + zOffset));

    }
}
