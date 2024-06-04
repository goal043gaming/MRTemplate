using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    //Materials that get changed by XR Grab Interactable events, used to change the color
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material selectMaterial; 

    //Material the objects starts with, used for reverting after selection
    private Material startMaterial;

    //Renderer of the current object, used to change the linked materials
    private Renderer objectRenderer;

    //Strings that get used in the PlacementHandler script in order to display information
    public string ObjectName;
    public string ObjectDescription;

    //Assign the renderer of the object and the starting material
    void Start()
    {
       objectRenderer = GetComponent<Renderer>();
       startMaterial = objectRenderer.material;
    }

    //Called by XR Grab Interactable events in order to change the material
    public void HoverObject()
    {
        objectRenderer.material = hoverMaterial;
    }

    public void ExitHover()
    {
        objectRenderer.material = startMaterial;
    }

    public void SelectObject()
    {
        objectRenderer.material = selectMaterial;
    }
}
