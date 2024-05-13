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

    private Image prevImage;
    private int currentStepIndex;

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
