using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ValveManager : MonoBehaviour
{
    [SerializeField] XRKnob valveRotation;
    [SerializeField] FlowCheckpoints connectedCheckpoint;
    private float rotationValue;

    [SerializeField] GameObject ClosedUI;
    [SerializeField] GameObject OpenUI;

    private void Update()
    {
        rotationValue = valveRotation.value;
        SetState();
    }

    private void SetState()
    {
        if(rotationValue <= 0.9)
        {
            connectedCheckpoint.valveOpen = false;
            ClosedUI.SetActive(true);
            OpenUI.SetActive(false);
        }
        else if(rotationValue >= 1)
        {
            connectedCheckpoint.valveOpen = true;
            ClosedUI.SetActive(false);
            OpenUI.SetActive(true);
        }
    }
}
