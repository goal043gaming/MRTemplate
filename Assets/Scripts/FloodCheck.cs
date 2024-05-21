using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodCheck : MonoBehaviour
{

    [SerializeField] GameObject floodObject;
    [SerializeField] Flood flood;

    [SerializeField] FlowHandler flowHandler;

    [SerializeField] float timeToFlood;

    [SerializeField] ParticleSystem leakParticle;

    [SerializeField] FlowStop flowStop;

    [SerializeField] AudioSource waterRunning;

    private bool isPlaying = false;
    private bool isFinish = false;

    private void OnTriggerEnter(Collider other)
    {
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

    private IEnumerator StartFlooding()
    {
        yield return new WaitForSeconds(timeToFlood);

        floodObject.SetActive(true);
        flood.flooding = true;
        flood.timerRunning = true;
    }

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
