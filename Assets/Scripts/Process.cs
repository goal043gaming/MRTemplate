using BestHTTP.SecureProtocol.Org.BouncyCastle.Asn1.Mozilla;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Process : MonoBehaviour
{
    //Objectlist script linked to the current process, used to determine the current step of the process
    public ObjectList objectList;

    //Text field used to display the currentobjective from the current step
    public TMP_Text objectText;

    //Image field used to display the currentsprite of the step
    public Image objectImage;

    //Array of gameobjects linked to the steps in the installation gets used in FindObjects
    public GameObject[] linkedObjects;

    //Text field to display the feedback text for the current step
    public TMP_Text feedbackText;

    //NO LONGER USED
    public string flowIdentifier, ballIdentifier;

    //Bool to determine if the step is active, gets disabled by the objectstate script
    public bool isActive;

    //UI Gameobject, gets enabled once the process has been completed, called in scenetransition
    [SerializeField] GameObject nextScene;

    //Int to check what the current step is, used in the nextstep function
    private int currentStepIndex;

    //Objectstate script to changes linked objects to active for selection, used in the display step function
    private ObjectState objectstate;

    //Object script used to get information from the scriptable object that is currently active
    private Object currentStep;

    //Debug code to test functionality without VR Input
    public bool debug;
    public bool lastCheck;

    //Function that gets called by the placementhandler script once all of the required objects have been placed
    //Function selects the current step, linked object if the index is less than the total amount
    //This changes all of the uI elements to the attached objects information and switches it to active
    //It then checks al of the identifier from linked objects assigned by the findobjects function to check for identifier and changes the attached bool in objectstate to correct
    //If the index has reched its end the nextscene gameobject gets enabled
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

                //NO LONGER USED
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
            nextScene.SetActive(true);
            print("End of the process");
        }
    }

    //Function that gets called by the placementhandler tool to progress to the next step or once the correct object has been selected in objectstate
    //This adds to the current index and calls the displaystep function
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
        if(lastCheck)
        {
            SceneTransition();
        }
    }

    //Function gets called by the placementhandler tool once the miniplant has been created, it adds all objects with the tag interactable to linkedobjects
    public void FindObjects()
    {
        linkedObjects = GameObject.FindGameObjectsWithTag("Interactable");
    }

    //Function that gets called once the process has ended and the nextscene Ui gameobject needs to be enabled
    public void SceneTransition()
    {
        nextScene.SetActive(true);
    }
}
