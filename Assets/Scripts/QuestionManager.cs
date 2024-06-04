using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    //List of QnaHolder script in order to create the questions, anwswers and assign the right answer to each of the questions
    [SerializeField] List<QnAHolder> qAHolder;

    //Array of Gameobjects linked that are tied to the possible answers used in the qAholder
    [SerializeField] GameObject[] options;

    //Array of text fields that gets used in the setanswers script, uses the QnaHolder to assign the answers to the text fields
    [SerializeField] TMP_Text[] answerText;

    //Text field that gets called in generate question in order to change the textfield to the current question from qaholder
    [SerializeField] TMP_Text questionText;

    //Spawnball script that is used in Generate question to give the player a sphere
    [SerializeField] SpawnBall ballSpawner;

    //Audio effect called in generate question
    [SerializeField] AudioSource writingQuestion;

    //Int to track what the current question is and progress to the next
    private int currentQuestionIndex = 0;

    //Testing bool to test without VR functionality
    public bool t_Correct;

    [Header("Objectives")]
    //ObjectiveHandler script used in order to change the current objective
    [SerializeField] ObjectiveHandler objective;

    //Array of strings to use to change the current objective
    [SerializeField] string[] objectivesToDisplay;

    //UI Gameobject that gets enabled if all of the questions have been answered
    [SerializeField] GameObject nextScene;

    //Function that gets called by the Correcto ienumerator, this happens once a question has been answered succesfully
    //It changes the current question to the one provided by qaholder, plays the sound effect, calls the SetAnswers function and the linked ballspawner Spawnprefab function and updated the objectives
    //If the questions are all answered the function enables the nextscene gameobject
    public void GenerateQuestion()
    {
        if(currentQuestionIndex >= qAHolder.Count)
        {
            nextScene.SetActive(true);
            return;
        }

        questionText.text = qAHolder[currentQuestionIndex].questions;

        writingQuestion.Play();
        SetAnswers();
        ballSpawner.SpawnPrefab();
        objective.UpdateText(objectivesToDisplay[0]);
    }

    //Function gets called by the generate question function after a question has been answered succesfully
    //This for loop gets all of the linked options and assign the correct answers and textfields according to the index and the attached qAholder
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

    //Function that gets called in Holedetection once a sphere has entered the collision of the linked targets
    public void Correct()
    {
        StartCoroutine(Correcto());
    }

    //IEnumerator called by the correct function, waits for a seconds, changes the index for the current question and calls the GenerateQuestion function
    public IEnumerator Correcto()
    {
        yield return new WaitForSeconds(1);
        currentQuestionIndex++;
        GenerateQuestion();
        //process.NextStep();
    }

    private void Update()
    {
        if(t_Correct)
        {
            Correct();
        }
    }
}
