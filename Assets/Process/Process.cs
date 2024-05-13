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

            if(currentStep.hasMicrogame)
            {
                StartMicrogame();
            }

            for(int i = 0; i < linkedObjects.Length; i++)
            {
                objectstate = linkedObjects[i].GetComponent<ObjectState>();
                string currentIdentifier = objectstate.uniqueIdentifier;

                if(currentStep.identifier == currentIdentifier)
                {
                    objectstate.isActive = true;
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
        if(currentStep.flowHandler != null)
        {
            print("Start the flow");
        }
        if(currentStep.questionManager != null)
        {
            print("Start the throwing");
        }
    }
}
