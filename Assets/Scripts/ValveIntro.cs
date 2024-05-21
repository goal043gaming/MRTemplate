using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveIntro : MonoBehaviour
{
    [SerializeField] GameObject connectedLid;
    [SerializeField] XRKnob XRKnob;
    public float forceValue;

    private float rotValue;
    private Rigidbody rb;
    private XRGrabInteractable interactable;
    private bool isLaunched = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = connectedLid.GetComponent<Rigidbody>();
        interactable = connectedLid.GetComponent<XRGrabInteractable>();

        interactable.enabled = false;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        rotValue = XRKnob.value;
        CheckRotation();
    }

    private void CheckRotation()
    {
        if(rotValue >= 1 && !isLaunched)
        {
            rb.isKinematic = false;
            //rb.AddForce(transform.up * forceValue);
            rb.AddForce(0, forceValue, 2, ForceMode.Impulse);
            isLaunched = true;
            interactable.enabled = true;
        }
    }
}
