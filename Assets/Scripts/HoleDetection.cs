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
            text.gameObject.SetActive(true);
            text.text = pointAmount.ToString();
            StartCoroutine(TextTimer(3));
            Destroy(collision.gameObject);

            /*
            if (collision.gameObject.GetComponent<BallBoosters>().boosterTriggered == true)
            {
                Destroy(collision.gameObject);
                pointAmount *= collision.gameObject.GetComponent<BallBoosters>().boosterAmount;
                text.text = pointAmount.ToString();
            } */
        }
    }

    private void OnEnable()
    {
        //Instantiate(pointBooster, transform.position+(transform.forward*2), transform.rotation);
        text.gameObject.SetActive(false);
    }

    private IEnumerator TextTimer(int Timer)
    {
        yield return new WaitForSeconds(Timer);
        text.gameObject.SetActive(false);
    }
}
