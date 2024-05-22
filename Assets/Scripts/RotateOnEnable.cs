using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        transform.Rotate(90, 0, 0);
    }
}
