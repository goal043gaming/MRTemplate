using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnEnable : MonoBehaviour
{
    //Debug bool to test functionality without VR input
    public bool debugRot;

    //Function that gets called by the spawnholes script once the ballspawner object has been placed
    //This function changes the rotation of the object to 0 on all axis
    public void RotateObject()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void Update()
    {
        if (debugRot)
        {
            RotateObject();
        }
    }
}
