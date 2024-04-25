using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform locationToSpawn;

    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, locationToSpawn.position, Quaternion.identity);
    }
}
