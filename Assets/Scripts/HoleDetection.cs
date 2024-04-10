using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{

    [SerializeField] int pointAmount;
    [SerializeField] TMP_Text text;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Throwable")
        {
            Destroy(collision.gameObject);
            text.text = pointAmount.ToString();
        }
    }
}
