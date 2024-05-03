using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Flood : MonoBehaviour
{

    [SerializeField] float speed = 1f;
    [SerializeField] float maxHeight = 5f;
    [SerializeField] float bottomHeight = 0f;

    [HideInInspector] public bool flooding = false;
    private bool isPlaying = false;

    public bool timerRunning;
    [SerializeField] float graceTime;
    private bool canDestroy = false;
    [HideInInspector] public bool hasFlooded = false;

    [SerializeField] AudioSource audio;

    [Header("Debug")]
    public bool debug = false;
    public bool isDebug = false;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= maxHeight)
        {
            flooding = false;
            audio.Stop();
            hasFlooded = true;
        }
        if(transform.position.y <= bottomHeight && canDestroy)
        {
            Destroy(gameObject);
        }

        if(flooding)
        {
            StartAudio();
            StartTimer();
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(debug && !isDebug)
        {
            TakeWater(10);
            isDebug = true;
        }
    }

    public void StartAudio()
    {
        if(isPlaying)
        {

        }
        else
        {
            audio.Play();
        }
    }

    public void TakeWater(float amount)
    {
        transform.Translate(Vector3.down / amount);
    }

    private void StartTimer()
    {
        if(timerRunning)
        {
            if(graceTime > 0)
            {
                graceTime -= Time.deltaTime;
            }
            else
            {
                timerRunning = false;
                graceTime = 0;
                print(" testing");
            }
        }
    }
}
