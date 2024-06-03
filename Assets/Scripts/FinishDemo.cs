using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDemo : MonoBehaviour
{
    //This script is used in the close the valves scene as a fail state once the flood has stopped

    //Get the flowHandler script used in the Close the Valves scene
    [SerializeField] FlowHandler flowHandler;

    //Gets the gameobject UI to restart the scene
    [SerializeField] GameObject demoEnding;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flow")
        {
            flowHandler.allowMovement = false;
            demoEnding.SetActive(true);
        }
    }
}
