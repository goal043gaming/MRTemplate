using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flood : MonoBehaviour
{

    public float speed = 1f;
    public float maxHeight = 5f;

    public bool flooding = false;
    private bool isPlaying = false;
    public bool hasFlooded = false;

    [SerializeField] AudioSource audio;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= maxHeight)
        {
            flooding = false;
            audio.Stop();
            hasFlooded = true;
        }

        if(flooding)
        {
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
