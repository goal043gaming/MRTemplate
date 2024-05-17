using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ValveIntro : MonoBehaviour
{
    [SerializeField] GameObject connectedLid;
    [SerializeField] XRKnob XRKnob;
    public float forceValue;

    private float rotValue;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = connectedLid.GetComponent<Rigidbody>();
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
        if(rotValue >= 1)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.up * forceValue);
        }
    }
}
