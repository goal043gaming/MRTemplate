using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    [SerializeField] Transform locationToSpawn;
    [SerializeField] GameObject uiDisable;

    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, locationToSpawn.position, Quaternion.identity);
        uiDisable.SetActive(false);
    }
}
