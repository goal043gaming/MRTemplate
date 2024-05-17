using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{
    [SerializeField] GameObject pointBooster;

    [SerializeField] int startingPoints;
    private int pointsToAdd;
    private GameObject playerPos;

    [SerializeField] TMP_Text text;
    [SerializeField] AnswerManager answerManager;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Throwable" && answerManager.isCorrect)
        { 
            if (collision.gameObject.GetComponent<BallBoosters>().boosterTriggered == true)
            {
                text.gameObject.SetActive(true);
                StartCoroutine(TextTimer(1));
                pointsToAdd = startingPoints * collision.gameObject.GetComponent<BallBoosters>().boosterAmount;
                text.text = pointsToAdd.ToString();
                Destroy(collision.gameObject);

                answerManager.Answer();
            }
            else
            {
                text.gameObject.SetActive(true);
                StartCoroutine(TextTimer(1));
                text.text = startingPoints.ToString();
                Destroy(collision.gameObject);

                answerManager.Answer();
            }
        }
        if(collision.transform.tag == "Throwable" && !answerManager.isCorrect)
        {
            text.gameObject.SetActive(true);
            StartCoroutine(TextTimer(1));
            text.text = "0";
            Destroy(collision.gameObject);

            answerManager.Answer();
        }
    }

    /* private void OnEnable()
    {
        var test = Instantiate(pointBooster, transform.position+(-transform.forward*1), transform.rotation);
        playerPos = GameObject.FindGameObjectWithTag("MainCamera");
        test.transform.LookAt(playerPos.transform.position);
        text.gameObject.SetActive(false);
    } */

    private IEnumerator TextTimer(int Timer)
    {
        yield return new WaitForSeconds(Timer);
        text.gameObject.SetActive(false);
    }
}
