using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFallingRocks : MonoBehaviour
{
    public FallingRocksSpawner frs;
    public int rockNumberLimit = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallingRocks"))
        {
            Destroy(other.gameObject);
            if (frs.transform.childCount < rockNumberLimit)
            {
                frs.SpawnRock();
            }
        }
    }
}
