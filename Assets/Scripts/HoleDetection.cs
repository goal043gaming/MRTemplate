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
                text.gameObject.SetActive(true);
                StartCoroutine(TextTimer(1));
                pointAmount *= collision.gameObject.GetComponent<BallBoosters>().boosterAmount;
                text.text = pointAmount.ToString();
                Destroy(collision.gameObject);
            } 
        }
    }

    private void OnEnable()
    {
        Instantiate(pointBooster, transform.position+(-transform.forward*1), transform.rotation);
        text.gameObject.SetActive(false);
    }

    private IEnumerator TextTimer(int Timer)
    {
        yield return new WaitForSeconds(Timer);
        text.gameObject.SetActive(false);
    }
}
