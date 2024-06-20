using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] XRRayInteractor leftRayInteractor;
    [SerializeField] XRRayInteractor rightRayInteractor;

    public void CloseWindow(GameObject gameObject)
    {
        gameObject.SetActive(false);
        leftRayInteractor.enableUIInteraction = false;
        rightRayInteractor.enableUIInteraction = false;
    }
}
