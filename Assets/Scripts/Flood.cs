using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Flood : MonoBehaviour
{
    //The speed at which the flood happens
    [SerializeField] float speed = 1f;
    
    //The maximum height of the flood in the scene
    [SerializeField] float maxHeight = 5f;

    //The minimum height of the flood in the scene
    [SerializeField] float bottomHeight = 0f;

    //public bool that is checked in other script but should not be changed
    [HideInInspector] public bool flooding = false;

    //NO LONGER USED
    //bool to check if audio is playing
    private bool isPlaying = false;

    //Timer that start to disable the flood from instantly disappearing if the bucket is on the ground
    public bool timerRunning;
    [SerializeField] public float graceTime;

    //bool that is enabled once a certain amount of time has passed
    public bool canDestroy = false;
    [HideInInspector] public bool hasFlooded = false;

    //NO LONGER USED
    [SerializeField] AudioSource audio;

    //UI object that is enabled in order to transition between scenes
    [SerializeField] GameObject sceneTransition;


    //Debug menu to test functionality without VR headset
    [Header("Debug")]
    public bool debug = false;
    public bool isDebug = false;

    // Update is called once per frame
    void Update()
    {
        //checking if the current position of the flood has surpassed the maximum, then it stops
        if(transform.position.y >= maxHeight)
        {
            flooding = false;
            audio.Stop();
            hasFlooded = true;
        }
        //check if the height is below the bottomheight and if it can be destroyed currently
        if(transform.position.y <= bottomHeight && canDestroy)
        {
            sceneTransition.SetActive(true);
            Destroy(gameObject);
        }

        //bool that is handled in other script like floodcheck and flowstop
        if(flooding)
        {
            //StartAudio();
            StartTimer();

            //move the object up while flooding is true
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if(debug && !isDebug)
        {
            TakeWater(10);
            isDebug = true;
        }
    }

    //NO LONGER USED
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

    //gets called by the bucket script
    public void TakeWater(float amount)
    {
        //decrease the height by the amount communicated
        transform.Translate(Vector3.down / amount);
    }

    //gets called once flooding starts, once the variable gracetime is below 0 the flood can be destroyed
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
                canDestroy = true;
                graceTime = 0;
                print(" testing");
            }
        }
    }
}
