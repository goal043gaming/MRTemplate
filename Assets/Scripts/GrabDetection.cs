using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    public bool isGrabbed;

    public void ObjectSelected()
    {
        isGrabbed = true;
    }
    public void ObjectDropped()
    {
        isGrabbed = false;
    }
}
