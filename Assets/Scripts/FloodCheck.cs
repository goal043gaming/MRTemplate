using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodCheck : MonoBehaviour
{
    //The gameobject that needs to be enabled once the flood starts
    [SerializeField] GameObject floodObject;

    //the flood script
    [SerializeField] Flood flood;

    //the flow script that needs to be stopped once a check has been passed
    [SerializeField] FlowHandler flowHandler;

    //the delay for the flood to start
    [SerializeField] float timeToFlood;

    //the linked particleeffect that plays until the flood starts
    [SerializeField] ParticleSystem leakParticle;

    //the flowstop script, used to communicate with the attached button
    [SerializeField] FlowStop flowStop;

    //audio effect that plays while the flood is happening
    [SerializeField] AudioSource waterRunning;

    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        //Checks if it has collided with the flow tag, inside the close the valves installation
        //if it has collided stop the movement and start the leak effects, communicates to flowstop what particle should play and that it should be enabled
        if(other.transform.tag == "Flow")
        {
            flowHandler.allowMovement = false;
            StartAudio();
            leakParticle.Play();
            flowStop.activeEffect = leakParticle;
            flowStop.ButtonEnabled = true;
            StartCoroutine(StartFlooding());
        }
    }

    //ienumerator that starts after a small delay and activates the flooding effect and communicates this with the flood
    private IEnumerator StartFlooding()
    {
        yield return new WaitForSeconds(timeToFlood);

        floodObject.SetActive(true);
        flood.flooding = true;
        flood.timerRunning = true;
    }

    //Audio that starts flooding sound using IEnumerator to disable the sound after the delay
    private void StartAudio()
    {
        waterRunning.Play();
        StartCoroutine(EnableAudio());
    }

    private void Update()
    {
        if(isPlaying && !flood.flooding)
        {
            waterRunning.Stop();
        }
    }

    private IEnumerator EnableAudio()
    {
        yield return new WaitForSeconds(timeToFlood);

        isPlaying = true;
    }
}
