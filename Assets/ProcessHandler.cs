using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcessHandler : MonoBehaviour
{
    public StepProcess process;
    public TMP_Text stepText;

    private int currentStepIndex = 0;

    public bool debug = false;

    // Start is called before the first frame update
    void Start()
    {
        DisplayStep(currentStepIndex);
    }

    void DisplayStep(int index)
    {
        if(index < process.steps.Length)
        {
            Step currentStep = process.steps[index];
            stepText.text = currentStep.stepDescription;
        }
        else
        {
            print("End of process");
        }
    }

    public void NextStep()
    {
        currentStepIndex++;
        DisplayStep(currentStepIndex);
    }

    private void FixedUpdate()
    {
       if(debug)
        {
            NextStep();
        }
    }
}
