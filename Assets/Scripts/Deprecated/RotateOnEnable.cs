using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnEnable : MonoBehaviour
{
    public bool debugRot;

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
