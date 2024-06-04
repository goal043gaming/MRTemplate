using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LaunchBall : MonoBehaviour
{
    //Bool to check if the ball is currently held by the player, called by the XR Grab Interactable functions
    public bool beingHeld;

    //Input used the default action input settings, can be changed in inspector
    [SerializeField] InputActionProperty input;

    //Float to determine the amount of speed that needs to be given to the ball
    [SerializeField] float speed;

    //Interactable field that gets disabled on launching the ball to prevent regrabbing the ball while launching
    private XRGrabInteractable interactable;

    //Rigidbody of the ball that needs to be launched
    private Rigidbody rb;

    private void Start()
    {
        //Assigns the rigidbody and grab interactable of the sphere object
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //If the object is grabbed and the input is given call the launchprojectile function
        if (beingHeld)
        {
            if (input.action.WasPerformedThisFrame())
            {
                LaunchProjectile();
            }
        }
    }

    //Function that gets called by the XR Grab Interactable on select function
    public void PickedUp()
    {
        beingHeld = true;
    }

    //Function that gets called by the XR Grab Interactble on select exited function
    public void Dropped()
    {
        beingHeld = false;
    }

    //Function that calls the IEnumerator in order to launch the sphere object
    private void LaunchProjectile()
    {
        StartCoroutine(LaunchSequence());
    }

    //IEnumerator that disables the XR GRab interactable for a short while and launches the sphere forwards
    private IEnumerator LaunchSequence()
    {
        interactable.enabled = false;

        yield return new WaitForFixedUpdate(); 

        rb.velocity = transform.forward * speed;

        yield return new WaitForSeconds(1f);

        interactable.enabled = true;
    }
}

