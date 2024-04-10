using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBoosters : MonoBehaviour
{
    public int boosterAmount = 2;
    public bool boosterTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        boosterTriggered = true;
    }
}
