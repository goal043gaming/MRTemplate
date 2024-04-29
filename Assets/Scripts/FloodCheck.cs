using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodCheck : MonoBehaviour
{

    [SerializeField] GameObject floodObject;
    [SerializeField] Flood flood;

    [SerializeField] FlowHandler flowHandler;

    [SerializeField] float timeToFlood;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flow")
        {
            flowHandler.allowMovement = false;
            StartCoroutine(StartFlooding());
        }
    }

    private IEnumerator StartFlooding()
    {
        yield return new WaitForSeconds(timeToFlood);

        floodObject.SetActive(true);
        flood.flooding = true;
    }
}