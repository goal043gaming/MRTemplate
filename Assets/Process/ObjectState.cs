using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectState : MonoBehaviour
{
    public bool isActive,isCorrect, isFalse, isLast;
    public string uniqueIdentifier;
    public GameObject FeedbackWindow;
    public TMP_Text correctText;

    [SerializeField] Process process;
    public void HasSelected()
    {
        if(isCorrect)
        {
            correctText.text = "Correct!";
            isCorrect = false;
            FeedbackWindow.SetActive(true);
            StartCoroutine(DisableWindow());
            
        }
       /* else
        {
            process.NextStep();
            correctText.text = "False!";
            FeedbackWindow?.SetActive(true);
            StartCoroutine(DisableWindow());
        } */
    }

    private IEnumerator DisableWindow()
    {
        yield return new WaitForSeconds(3);
        FeedbackWindow.SetActive(false);
        process.NextStep();
    }
}
