using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishDemo : MonoBehaviour
{
    [SerializeField] FlowHandler flowHandler;
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
