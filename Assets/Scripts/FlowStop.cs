using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlowStop : MonoBehaviour
{
    [SerializeField] Flood flood;
    [SerializeField] FlowHandler flowHandler;

    public bool ButtonEnabled = false;
    public ParticleSystem activeEffect;

    [SerializeField] float thresholdValue = 0.1f;
    [SerializeField] float deadzone = 0.025f;

    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    [Header("Debug")]
    public bool PressButton = false;

    private void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (PressButton)
        {
            ButtonPress();
        }

        if(!isPressed && GetValue() + thresholdValue >= 1)
        {
            ButtonPress();
        }
        if(isPressed && GetValue()  - thresholdValue <= 0)
        {
            ButtonRelease();
        }

    }
    private void ButtonPress()
    {
        isPressed = true;

        if (ButtonEnabled)
        {
            flood.flooding = false;
            flowHandler.allowMovement = false;
            activeEffect.Stop();
        }
    }

    private void ButtonRelease()
    {
        isPressed = false;
    }

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
