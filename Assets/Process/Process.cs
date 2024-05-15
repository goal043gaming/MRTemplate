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

    private Image prevImage;
    private int currentStepIndex;
    private ObjectState objectstate;
    private Object currentStep;

    public bool debug;

    private void Start()
    {
        DisplayStep(currentStepIndex);
    }

    public void DisplayStep(int index)
    {
        if(index < objectList.objects.Count)
        {
            currentStep = objectList.objects[index];
            objectText.text = currentStep.attachedObjective;
            objectImage.sprite = currentStep.attachedSprite;
            feedbackText.text = currentStep.feedbackText;

            for(int i = 0; i < linkedObjects.Length; i++)
            {
                objectstate = linkedObjects[i].GetComponent<ObjectState>();
                string currentIdentifier = objectstate.uniqueIdentifier;

                if(currentStep.hasMicrogame)
                {
                    if(currentStep.identifier == flowIdentifier)
                    {
                        linkedObjects[i].GetComponent<FlowHandler>().StartFlow();
                        objectstate.isActive = true;
                        break;
                    }
                    else if(currentStep.identifier == ballIdentifier)
                    {
                        linkedObjects[i].GetComponent<QuestionManager>().GenerateQuestion();
                        objectstate.isActive = true;
                        break;
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

    public void StartMicrogame()
    {
        
    }
}
