using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeposit : MonoBehaviour
{
    //Bucket script link to the bucket in the scene used to call the EmptyBucket function in the collision event
    [SerializeField] Bucket existingBucket;

    //Audio source used in the collision event once the water has been deposited
    [SerializeField] AudioSource waterDrop;

    //Collision event that triggers once the collider has collided with an object tagged with water, calls the function emptybucket in the bucket script and plays the sound
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Water")
        {
            existingBucket.EmptyBucket();
            waterDrop.Play();
        }
    }
}
