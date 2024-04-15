using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] List<QnAHolder> qAHolder;
    [SerializeField] GameObject[] options;
    public int currentQuestion;
    [SerializeField] TMP_Text questionText;

    public void GenerateQuestion()
    {
        currentQuestion = Random.Range(0, qAHolder.Count);
        questionText.text = qAHolder[currentQuestion].questions;

        SetAnswers();
    }

    public void SetAnswers()
    {
        for(int i = 0; i < options.Length ; i++)
        {
            options[i].GetComponent<AnswerManager>().isCorrect = false;
            //options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = qAHolder[currentQuestion].answers[i];

            if (qAHolder[currentQuestion].correctAnswer == i+1)
            {
                options[i].GetComponent<AnswerManager>().isCorrect = true;
                print(options[i] + "This is it");
            }
        }
    }

    public void Correct()
    {
        // qAHolder.RemoveAt(currentQuestion);
        // GenerateQuestion();
        print("testing");
    }
}
