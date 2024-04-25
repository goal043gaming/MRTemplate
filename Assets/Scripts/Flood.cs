using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{

    public float speed = 1f;
    public float maxHeight = 5f;

    private bool flooding = true;
    private bool isPlaying = false;

    [SerializeField] AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= maxHeight)
        {
            flooding = false;
            audio.Stop();
            isPlaying = false;
        }

        if(flooding)
        {
            isPlaying = true;
            StartAudio();
            transform.Translate(Vector3.up * speed * Time.deltaTime);
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
}
