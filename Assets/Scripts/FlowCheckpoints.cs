using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowCheckpoints : MonoBehaviour
{
    public bool checkPointPassed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flow")
        {
            checkPointPassed = true;
        }
        else
        {
            checkPointPassed = false;
        }   
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Flow")
        {
            checkPointPassed = false;
        }
    }
}
