using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HoleDetection : MonoBehaviour
{
    //NO LONGER USED
    [SerializeField] GameObject pointBooster;

    //Int value for the starting amount of points received on getting the right answer
    [SerializeField] int startingPoints;

    //Int to determine the amount of points that needed to be added, used by the pointbooster system
    private int pointsToAdd;

    //NO LONGER USED
    private GameObject playerPos;

    //Text field in order to display the amount of points received for getting the answer
    [SerializeField] TMP_Text text;

    //Answermanager script that gets called to check the state of the target and to communicate that a question has been answered
    [SerializeField] AnswerManager answerManager;

    private void OnCollisionEnter(Collision collision)
    {
        //Collision check for throwable tagged object and if the target is assigned as the correct answer
        //Enables the text object, assigns the amount of points received for the answer and destroys the collision object, boostertrigger is no longer used
        if(collision.transform.tag == "Throwable" && answerManager.isCorrect)
        { 
            if (collision.gameObject.GetComponent<BallBoosters>().boosterTriggered == true)
            {
                text.gameObject.SetActive(true);
                StartCoroutine(TextTimer(2));
                pointsToAdd = startingPoints * collision.gameObject.GetComponent<BallBoosters>().boosterAmount;
                text.text = "Dat is correct!";
                Destroy(collision.gameObject);

                answerManager.Answer();
            }
            else
            {
                text.gameObject.SetActive(true);
                StartCoroutine(TextTimer(2));
                text.text = "Dat is correct!";
                Destroy(collision.gameObject);

                answerManager.Answer();
            }
        }
        //Collision check for the throwable tag and if the answer is not the right one
        //Changes points received to 0 and does the rest the same
        if(collision.transform.tag == "Throwable" && !answerManager.isCorrect)
        {
            text.gameObject.SetActive(true);
            StartCoroutine(TextTimer(2));
            text.text = "Dat is fout!";
            Destroy(collision.gameObject);

            answerManager.Answer();
        }
    }

    //IEnumerator to disable the text after a short delay, timer given by the call
    private IEnumerator TextTimer(int Timer)
    {
        yield return new WaitForSeconds(Timer);
        text.gameObject.SetActive(false);
    }
}
