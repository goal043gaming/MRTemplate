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

    public bool debug;

    private void Start()
    {
        DisplayStep(currentStepIndex);
    }

    public void DisplayStep(int index)
    {
        if(index <= objectList.objects.Count)
        {
            Object currentStep = objectList.objects[index];
            objectText.text = currentStep.attachedObjective;
            objectImage.sprite = currentStep.attachedSprite;

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
}
