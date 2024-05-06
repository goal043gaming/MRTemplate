using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeposit : MonoBehaviour
{
    [SerializeField] Bucket existingBucket;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Water")
        {
            existingBucket.EmptyBucket();
        }
    }
}
