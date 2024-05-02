using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private bool isFull = false;

    [SerializeField][Range(5.0f, 20.0f)] float amountToSubtract;

    private Flood flood;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(transform.tag == "Flood" && !isFull)
        {
            flood = other.GetComponent<Flood>();
            flood.TakeWater(amountToSubtract);
            isFull = true;
            print("testing");
        }
    }
}
