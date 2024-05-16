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

    private int currentQuestionIndex = 0;
    public bool t_Correct;

    [Header("Objectives")]
    [SerializeField] ObjectiveHandler objective;
    [SerializeField] string[] objectivesToDisplay;

    [SerializeField] Process process;

    public void Start()
    {
        //GenerateQuestion();
    }

    public void GenerateQuestion()
    {
        if(currentQuestionIndex >= qAHolder.Count)
        {
            print("No more questions available.");
            return;
        }

        questionText.text = qAHolder[currentQuestionIndex].questions;

        writingQuestion.Play();
        SetAnswers();
        ballSpawner.SpawnPrefab();
        objective.UpdateText(objectivesToDisplay[0]);
    }

    public void SetAnswers()
    {
        for(int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerManager>().isCorrect = false;
            answerText[i].text = qAHolder[currentQuestionIndex].answers[i];

            if(qAHolder[currentQuestionIndex].correctAnswer == i + 1)
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
        currentQuestionIndex++;
        //GenerateQuestion();
        process.NextStep();
    }

    private void Update()
    {
        if(t_Correct)
        {
            Correct();
        }
    }
}
