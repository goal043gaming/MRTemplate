using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopScored : MonoBehaviour
{

    public ParticleSystem confettiParticle;

    private void OnTriggerEnter(Collider other)
    {
        confettiParticle.Play();
    }
}
