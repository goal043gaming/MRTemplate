using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ValveManager : MonoBehaviour
{
    //XRknob script that is used in order to get the rotation of the connected valve, used to detect opening state or closed state
    [SerializeField] XRKnob valveRotation;

    //Flowcheckpoint script to update the state of the valve to open or closed
    [SerializeField] FlowCheckpoints connectedCheckpoint;

    //Float value for the rotation of the valve object, inherited from the XRknob script
    private float rotationValue;

    //UI Gameobjects to display the current state of the valve, used in the SetState function
    [SerializeField] GameObject ClosedUI;
    [SerializeField] GameObject OpenUI;

    //Update function that gets the rotationvalve from the XRknob script and assigns it to the private variable of rotationvalue, then calls the SetState function
    private void Update()
    {
        rotationValue = valveRotation.value;
        SetState();
    }

    //Function that gets called by the update function, checks the rotationvalue to determine if the valve is open or closed and changes the UI based on its state
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
