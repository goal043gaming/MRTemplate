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
    

    [SerializeField] QuestionManager questionManager;
    public void Answer()
    {
        if(isCorrect)
        {
            questionManager.Correct();
            explosionR.Play();
        }
        else
        {
            explosionW.Play();
        }
    }

    public void Update()
    {
        if(isCorrect)
        {
            print("I AM CORRECT");
        }
    }
}
