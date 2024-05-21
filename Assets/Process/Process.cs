using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    public ObjectList objectList;
    public TMP_Text objectText;
    public Image objectImage;
    public GameObject[] linkedObjects;
    public TMP_Text feedbackText;
    public string flowIdentifier, ballIdentifier;
    public bool isActive;

    private Image prevImage;
    private int currentStepIndex;
    private ObjectState objectstate;
    private Object currentStep;

    public bool debug;

    private void Start()
    {
        //DisplayStep(currentStepIndex);
    }

    public void DisplayStep(int index)
    {
        if(index < objectList.objects.Count)
        {
            
            currentStep = objectList.objects[index];
            objectText.text = currentStep.attachedObjective;
            objectImage.sprite = currentStep.attachedSprite;
            feedbackText.text = currentStep.feedbackText;

            isActive = true;

            for(int i = 0; i < linkedObjects.Length; i++)
            {
                objectstate = linkedObjects[i].GetComponent<ObjectState>();
                string currentIdentifier = objectstate.uniqueIdentifier;

                if(currentStep.hasMicrogame)
                {
                    if(currentStep.ballGame)
                    {
                        if(currentStep.identifier == currentIdentifier)
                        {
                            print(linkedObjects[i]);
                            QuestionManager genQuestion = linkedObjects[i].GetComponent<QuestionManager>();
                            genQuestion.GenerateQuestion();
                            objectstate.isActive = true;
                            print("TEST");
                            break;
                        }
                    }
                    if(currentStep.flowGame)
                    {
                        if (currentStep.identifier == currentIdentifier)
                        {
                            linkedObjects[i].GetComponent<FlowHandler>().StartFlow();
                            objectstate.isActive = true;
                            break;
                        }
                    }
                }

                if(currentStep.identifier == currentIdentifier)
                {
                    objectstate.isCorrect = true;
                    break;
                }
            }
        }
        else
        {
            print("End of the process");
        }
    }

    public void NextStep()
    {
        currentStepIndex++;
        DisplayStep(currentStepIndex);
    }

    private void FixedUpdate()
    {
        if (debug)
        {
            NextStep();
        }
    }

    public void FindObjects()
    {
        linkedObjects = GameObject.FindGameObjectsWithTag("Interactable");
    }
}
