using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{
    [SerializeField] GameObject pointBooster;

    [SerializeField] int pointAmount;
    [SerializeField] TMP_Text text;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Throwable")
        {
            if (collision.gameObject.GetComponent<BallBoosters>().boosterTriggered == true)
            {
                Destroy(collision.gameObject);
                pointAmount *= collision.gameObject.GetComponent<BallBoosters>().boosterAmount;
                text.text = pointAmount.ToString();
            }
        }
    }

    private void OnEnable()
    {
        Instantiate(pointBooster, transform.position+(transform.forward*2), transform.rotation);
    }
}
