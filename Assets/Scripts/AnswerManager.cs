using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.XR.Interaction.Toolkit;

public class AnswerManager : MonoBehaviour
{
    public bool isCorrect = false;
    [SerializeField] VisualEffect explosionW;
    [SerializeField] VisualEffect explosionR;

    [SerializeField] SpawnBall ballSpawner;

    [SerializeField] QuestionManager questionManager;

    [SerializeField] AudioSource wrongAnswerAudio;
    [SerializeField] AudioSource rightAnswerAudio;
    public void Answer()
    {
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
