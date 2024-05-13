using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState : MonoBehaviour
{
    public bool isActive;
    public string uniqueIdentifier;

    [SerializeField] Process process;
    public void HasSelected()
    {
        if(isActive)
        {
            process.NextStep();
        }
    }
}
