using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    public bool isGrabbed;

    [SerializeField] AudioSource objectGrabbed;
    [SerializeField] AudioSource objectDropped;

    public void ObjectSelected()
    {
        isGrabbed = true;
        objectGrabbed.Play();
    }
    public void ObjectDropped()
    {
        isGrabbed = false;
        objectDropped.Play();
    }
}
