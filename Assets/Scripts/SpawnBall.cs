using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    //Gameobject that gets spawned in the SpawnPrefab function
    [SerializeField] GameObject prefabToSpawn;

    //Transform for the location that gets used in the Spawnprefab function
    [SerializeField] Transform locationToSpawn;

    //UI Gameobject that gets called in the SpawnPrefab function
    [SerializeField] GameObject uiDisable;

    //Function that is called by the QuestionManager script once a question has been answered succesfully
    //This function instanaties the linked prefab at the linked location and disables the linked UI gameobject
    public void SpawnPrefab()
    {
        Instantiate(prefabToSpawn, locationToSpawn.position, Quaternion.identity);
        uiDisable.SetActive(false);
    }
}
