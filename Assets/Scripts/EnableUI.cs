using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnableUI : MonoBehaviour
{
    //Linked UI menu that enables on pressing the buttons
    [SerializeField] GameObject uiToEnable;

    //Input actions to disable and enable, uses input from default button set
    [SerializeField] InputActionProperty inputEnable;
    [SerializeField] InputActionProperty inputDisable;

    //bool checking if the menu is already active
    private bool isEnabled = false;

    // Update is called once per frame
    void Update()
    {
        if(inputEnable.action.WasPressedThisFrame())
        {
            EnableSceneUI();
        }
        if(inputDisable.action.WasPressedThisFrame())
        {
            DisableSceneUI();
        }
    }

    private void EnableSceneUI()
    {
        if(!isEnabled)
        {
            uiToEnable.SetActive(true);
            isEnabled = true;
        }
    }

    private void DisableSceneUI()
    {
        if(isEnabled)
        {
            uiToEnable.SetActive(false);
            isEnabled = false;
        }
    }
}
