using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{

    [SerializeField] int pointAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Throwable")
        {
            Destroy(collision.gameObject);
        }
    }
}
