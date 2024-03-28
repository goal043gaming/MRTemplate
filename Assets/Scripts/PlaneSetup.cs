using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSetup : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public PlaneClassification[] planeClassification;

    //0 = Wall
    //1 = Floor
    //2 = Ceiling
    //3 = Table

    public Material[] planeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        planeManager.planesChanged += SetupPlanes;
    }
    private void OnDisable()
    {
        planeManager.planesChanged -= SetupPlanes;
    }

    private void SetupPlanes(ARPlanesChangedEventArgs args)
    {
        List<ARPlane> newPlanes = args.added;

        foreach (var item in newPlanes)
        {
            if(item.classification == planeClassification[0])
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();
                itemRenderer.material = planeMaterial[0];
            }
            if (item.classification == planeClassification[1])
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();
                itemRenderer.material = planeMaterial[1];
            }
            if (item.classification == planeClassification[2])
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();
                itemRenderer.material = planeMaterial[2];
            }
            if (item.classification == planeClassification[3])
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();
                itemRenderer.material = planeMaterial[3];
            }
        }
    }

}