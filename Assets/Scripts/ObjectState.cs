using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    //Booleans used in order to check the state of the current process object
    public bool isActive,isCorrect, isFalse, isLast;

    //String identifier to link to the current object and the game object in the scene
    public string uniqueIdentifier;

    //UI gameobect that gets enabled once the right question has been given
    public GameObject FeedbackWindow;

    //Text field that changes the text based on right or wrong
    public TMP_Text correctText;

    //Process script that is used in order to progress to the next step in the installation
    [SerializeField] Process process;
    
    //Function that gets called by XR Grab Interactable on select event
    //Function changes if the correct object has been selected, it then changes the text field, the state of a bool, enables the feedback window and then calls the coroutine
    public void HasSelected()
    {
        if(isCorrect)
        {
            correctText.text = "Correct!";
            isCorrect = false;
            FeedbackWindow.SetActive(true);
            StartCoroutine(DisableWindow());
            
        }
    }

    //IEnumerator used to disable the feedback window after a short delay and progress the process to the next step
    private IEnumerator DisableWindow()
    {
        //The number 3 was a generic number for the prototype, it provided a good balance between being able to read and not slowing it down too much
        yield return new WaitForSeconds(3);
        FeedbackWindow.SetActive(false);
        process.NextStep();
    }
}
