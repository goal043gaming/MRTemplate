using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LaunchBall : MonoBehaviour
{
    public bool beingHeld;

    [SerializeField] InputActionProperty input;
    [SerializeField] float speed;

    private XRGrabInteractable interactable;
    private Rigidbody rb;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (beingHeld)
        {
            if (input.action.WasPerformedThisFrame())
            {
                LaunchProjectile();
            }
        }
    }

    public void PickedUp()
    {
        beingHeld = true;
    }

    public void Dropped()
    {
        beingHeld = false;
    }

    private void LaunchProjectile()
    {
        StartCoroutine(LaunchSequence());
    }

    private IEnumerator LaunchSequence()
    {
        interactable.enabled = false;

        yield return new WaitForFixedUpdate(); 

        rb.velocity = transform.forward * speed;

        yield return new WaitForSeconds(1f);

        interactable.enabled = true;
    }
}

