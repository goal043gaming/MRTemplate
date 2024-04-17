using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour
{
    [SerializeField] Material hoverMaterial;
    private Material startMaterial;
    private Renderer objectRenderer;

    // Start is called before the first frame update
    void Start()
    {
       objectRenderer = GetComponent<Renderer>();
       startMaterial = objectRenderer.material;
    }

    public void HoverObject()
    {
        objectRenderer.material = hoverMaterial;
    }

    public void ExitHover()
    {
        objectRenderer.material = startMaterial;
    }
}
