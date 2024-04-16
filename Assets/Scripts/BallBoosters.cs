using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBoosters : MonoBehaviour
{
    public int boosterAmount = 2;
    public bool boosterTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Booster")
        {
            boosterTriggered = true;
            StartCoroutine(DisableMult(1));
        }
    }

    private IEnumerator DisableMult(float amount)
    {
        yield return new WaitForSeconds(amount);
        boosterTriggered = false;
    }
}
