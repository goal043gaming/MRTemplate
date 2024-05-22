using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnEnable : MonoBehaviour
{
    public void RotateObject()
    {
        transform.Rotate(90, 0, 0);
    }
}
