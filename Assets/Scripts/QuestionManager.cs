using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] List<QnAHolder> qAHolder;
    [SerializeField] GameObject[] options;
    [SerializeField] TMP_Text[] answerText;
    [SerializeField] TMP_Text questionText;
    [SerializeField] SpawnBall ballSpawner;
    [SerializeField] AudioSource writingQuestion;

    public int currentQuestion;
    public bool t_Correct;

    public void Start()
    {
        GenerateQuestion();
    }
    public void GenerateQuestion()
    {
        currentQuestion = Random.Range(0, qAHolder.Count);
        questionText.text = qAHolder[currentQuestion].questions;

        writingQuestion.Play();
        SetAnswers();
        ballSpawner.SpawnPrefab();
    }

    public void SetAnswers()
    {
        for(int i = 0; i < options.Length ; i++)
        {
            options[i].GetComponent<AnswerManager>().isCorrect = false;
            answerText[i].text = qAHolder[currentQuestion].answers[i];

            if (qAHolder[currentQuestion].correctAnswer == i+1)
            {
                options[i].GetComponent<AnswerManager>().isCorrect = true;
            }
        }
    }

    public void Correct()
    {
        StartCoroutine(Correcto());
    }

    public IEnumerator Correcto()
    {
        yield return new WaitForSeconds(1);
        qAHolder.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    private void Update()
    {
        if(t_Correct)
        {
            Correct();
        }
    }
}
