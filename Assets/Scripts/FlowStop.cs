using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowStop : MonoBehaviour
{
    //Flood script that is linked in order to manage the states
    [SerializeField] Flood flood;

    //Flowhandler script that stops the movement
    [SerializeField] FlowHandler flowHandler;

    //Bool to enable the button once the flooding has begun
    public bool ButtonEnabled = false;

    //Particle effect that is given by Floodcheck
    public ParticleSystem activeEffect;

    //NO LONGER USED
    //Used in order to create a button with rigidbody collision rather than interaction
    [SerializeField] float thresholdValue = 0.1f;
    [SerializeField] float deadzone = 0.025f;

    //NO LONGER USED
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    //Debug bool in order to test button functionality without VR
    [Header("Debug")]
    public bool PressButton = false;

    private void Update()
    {
        if (PressButton)
        {
            ButtonPress();
        }
    }

    //Function that gets called by the XR Grab Interactable script on the button
    //Function stops the flooding by addressing Flood script and stops particles and flow movement
    public void ButtonPress()
    {
        //isPressed = true;

        if (ButtonEnabled)
        {
            flood.flooding = false;
            flood.hasFlooded = true;
            flood.canDestroy = true;
            flowHandler.allowMovement = false;
            activeEffect.Stop();
        }
    }

    //NO LONGER USED
    private void ButtonRelease()
    {
        isPressed = false;
    }

    //NO LONGER USED
    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if(Mathf.Abs(value) < deadzone)
        {
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }
}
