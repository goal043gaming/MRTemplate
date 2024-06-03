using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class AnswerManager : MonoBehaviour
{
    //Public boolean that gets changed by the questionmanager
    public bool isCorrect = false;

    //VFX from VFXGraph to visualize the right and wrong answer
    [SerializeField] VisualEffect explosionW;
    [SerializeField] VisualEffect explosionR;

    //Linked script that instantiates balls
    [SerializeField] SpawnBall ballSpawner;

    //Questionmanager that manages the questions and the right or wrong state of individual objects
    [SerializeField] QuestionManager questionManager;

    //Sounds that play on the right or wrong answer
    [SerializeField] AudioSource wrongAnswerAudio;
    [SerializeField] AudioSource rightAnswerAudio;
    public void Answer()
    {
        //This gets changed by the questionmanager on generating a new question
        if(isCorrect)
        {
            questionManager.Correct();
            explosionR.Play();
            rightAnswerAudio.Play();
        }
        else
        {
            explosionW.Play();
            ballSpawner.SpawnPrefab();
            wrongAnswerAudio.Play();
        }
    }
}
