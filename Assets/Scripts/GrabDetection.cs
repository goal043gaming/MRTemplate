using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    //Bool that gets called by XR Grab Interactable in order to check in the placementmanager script
    public bool isGrabbed;

    //Audio used for when the object is grabbed and dropped for sounds
    [SerializeField] AudioSource objectGrabbed;
    [SerializeField] AudioSource objectDropped;

    //Function that gets called by the XR GRab Interactable on select enter event, changes the bool and plays the sound
    public void ObjectSelected()
    {
        isGrabbed = true;
        objectGrabbed.Play();
    }
    
    //Function that gets called by the XR Grab Interactable on select exit event, changes the bool and plays the sound
    public void ObjectDropped()
    {
        isGrabbed = false;
        objectDropped.Play();
    }
}
