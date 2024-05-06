using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private bool isFull = false;

    [SerializeField][Range(5.0f, 20.0f)] float amountToSubtract;
    [SerializeField] GameObject waterVisual;

    [SerializeField] AudioSource grabWaterAudio;

    private Flood flood;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flood" && !isFull)
        {
            flood = other.GetComponent<Flood>();
            flood.TakeWater(amountToSubtract);
            grabWaterAudio.Play();
            isFull = true;
            waterVisual.SetActive(true);
        }
    }
    public void EmptyBucket()
    {
        isFull = false;
        waterVisual.SetActive(false);
    }
}
