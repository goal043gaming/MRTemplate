using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowCheckpoints : MonoBehaviour
{
    //Bool that gets changed by the Flowhandler script once the water has passed the collision field
    public bool checkPointPassed = false;

    //Bool for the state of the valve, handled by the XRknob script
    public bool valveOpen = false;

    //Transform to give direction to the flow, based on the valve state
    [SerializeField] Transform rightDirection;
    [SerializeField] Transform wrongDirection;

    //Linked gameobject is the visualizer for the water effect, is required for the flowhandler
    [SerializeField] public GameObject linkedObject;

    //The target in which the object needs to move, gets changed based on the valve state
    public Transform curTarget;

    //float for how long the checkpoint get disabled after collision
    [SerializeField][Range(0,1)] float disableTimer;

    private void OnTriggerEnter(Collider other)
    {
        //On collision with a flow object will change the direction of the current movement target and disables the checkpoint
        if(other.transform.tag == "Flow")
        {
            if(valveOpen)
            {
                curTarget = rightDirection;
            }
            else if (!valveOpen)
            {
                curTarget = wrongDirection;
            }
            checkPointPassed = true;
            StartCoroutine(disableCheckPoint());
        }
        else
        {
            checkPointPassed = false;
        }   
    }

    //IEnumerator to disable the checkpoint for a short while to prevent direction changes
    private IEnumerator disableCheckPoint()
    {
        yield return new WaitForSeconds(disableTimer);
        checkPointPassed = false;
    }
}
