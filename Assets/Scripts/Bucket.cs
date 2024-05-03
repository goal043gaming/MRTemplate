using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour
{
    private bool isFull = false;

    [SerializeField][Range(5.0f, 20.0f)] float amountToSubtract;
    [SerializeField] GameObject waterVisual;

    private Flood flood;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flood" && !isFull)
        {
            flood = other.GetComponent<Flood>();
            flood.TakeWater(amountToSubtract);
            isFull = true;
            waterVisual.SetActive(true);
        }
    }
}
