using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public bool isActive;
    public string uniqueIdentifier;
    public GameObject FeedbackWindow;

    [SerializeField] Process process;
    public void HasSelected()
    {
        if(isActive)
        {
            process.NextStep();
            isActive = false;
            FeedbackWindow.SetActive(true);
            StartCoroutine(DisableWindow());
        }
    }

    private IEnumerator DisableWindow()
    {
        yield return new WaitForSeconds(3);
        FeedbackWindow.SetActive(false);
    }
}
