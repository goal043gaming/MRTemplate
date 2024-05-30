using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnCube : MonoBehaviour
{
    public GameObject cubePrefab;
    public float speed = 2;
    public InputActionProperty input;

    // Update is called once per frame
    void Update()
    {
        if(input.action.WasPressedThisFrame())
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject spawnedCube = Instantiate(cubePrefab, transform.position, transform.rotation);
        Rigidbody rb = spawnedCube.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
}
