using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneDetection : MonoBehaviour
{

    public ARPlaneManager planeManager;
    public PlaneClassification planeClassification;

    private void OnEnable()
    {
        planeManager.planesChanged += SetupPlane;
    }
    private void OnDisable()
    {
        planeManager.planesChanged -= SetupPlane;
    }

    private void SetupPlane(ARPlanesChangedEventArgs args)
    {
        List<ARPlane> newPlane = args.added;

        foreach(var item in newPlane)
        {
            if(item.classification == planeClassification)
            {

            }
            else
            {
                Renderer itemRenderer = item.GetComponent<Renderer>();
                Destroy(itemRenderer);
            }
        }
    }
}
