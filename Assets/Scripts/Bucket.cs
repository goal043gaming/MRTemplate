using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    //Bool that gets changed on colliding with the water effect.
    private bool isFull = false;

    //float that communicates to the flood script how much water, less is more
    [SerializeField][Range(5.0f, 20.0f)] float amountToSubtract;

    //gameobject that gets enabled once the bucket is full, has a small water visual
    [SerializeField] GameObject waterVisual;

    //Audio effect on grabbing the water
    [SerializeField] AudioSource grabWaterAudio;

    //UI object on the water dispenser that enables on having the water
    [SerializeField] GameObject uiHelp;

    //Water script that communicates to decrease the value
    private Flood flood;

    private void OnTriggerEnter(Collider other)
    {
        //Flood object needs the tag for collision and will only happen once the bucket is not already full
        if(other.transform.tag == "Flood" && !isFull)
        {
            flood = other.GetComponent<Flood>();
            flood.TakeWater(amountToSubtract);
            grabWaterAudio.Play();
            isFull = true;
            waterVisual.SetActive(true);
        }
    }
    //This gets called by the water dispenser script with the name: WaterDeposit
    public void EmptyBucket()
    {
        isFull = false;
        waterVisual.SetActive(false);
    }
}
