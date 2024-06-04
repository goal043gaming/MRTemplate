using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveIntro : MonoBehaviour
{
    //Gameobject used in the start function and the checkrotation function
    [SerializeField] GameObject connectedLid;

    //XRknob script that is used to get the rotation value
    [SerializeField] XRKnob XRKnob;

    //float that determines how much power the rigidbody receives in the checkrotation function
    public float forceValue;

    //float that represents the rotation value of the valve, used in update and Checkrotation
    private float rotValue;

    //rigidbody of the connectlid, used to add force to in the checkrotation function
    private Rigidbody rb;

    //XR Grab Interactable script used to disable the interactable on start and after launching reenabling it
    private XRGrabInteractable interactable;

    //Bool to check if the lid has been launched yet, preventing it from looping or launching again
    private bool isLaunched = false;

    //Start function, attaches the rigidbody, the interactable script and sets the values to prevent the lid from moving until the valve has been turned
    void Start()
    {
        rb = connectedLid.GetComponent<Rigidbody>();
        interactable = connectedLid.GetComponent<XRGrabInteractable>();

        interactable.enabled = false;
        rb.isKinematic = true;
    }

    //Update function gets the rotValue from the attached XR Knob script and uses the publicly updating value variable to assign to rotvalue, calls the function checkrotation
    void Update()
    {
        rotValue = XRKnob.value;
        CheckRotation();
    }

    //Function is called in the update function, if the valve has been rotated into the right direction is launches the connectlid and enables interactivity functionality on it
    private void CheckRotation()
    {
        //The value is inherited from the XR Knob script, 1 means it's at its limit of rotation, similar to 0, tweak to the required rotation direction
        if(rotValue >= 1 && !isLaunched)
        {
            rb.isKinematic = false;
            rb.AddForce(0, forceValue, 2, ForceMode.Impulse);
            isLaunched = true;
            interactable.enabled = true;
        }
    }
}
