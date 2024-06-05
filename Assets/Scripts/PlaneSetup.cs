using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaneSetup : MonoBehaviour
{
    //PlaneManager script that is used in order to create the environment based on the spatial setup performed
    public ARPlaneManager planeManager;

    //Array of planeclassifcation codes from the spatial setup tool, gives the options for walls, windows, floor etc. Is used in order to assign colors or objects to it
    public PlaneClassification[] planeClassification;

    //Gameobject that gets placed on OnEnable  if the plane is " Window" 
    [SerializeField] GameObject[] objectToPlace;

    //List of Planes for the windows that gets created on SetupPlanes in order to identify all windows in the scene
    private List<ARPlane> Planes = new List<ARPlane>();

    //Bool that decides if the object needs to be placed on a window, gets changed in the inspector for specific scenes
    public bool placePrefab;

    //Array of materials to assign to the different planes identified
    public Material[] planeMaterial;

    private int index;

    //Function that gets called on enable, calls the setupplanes function
    private void OnEnable()
    {
        planeManager.planesChanged += SetupPlanes;
    }

    //Function that gets called on disable, calls the setupplanes function
    private void OnDisable()
    {
        planeManager.planesChanged -= SetupPlanes;
    }

    //Function that gets called on disable and enable and it gathers all of the planes in the scene from the spatial setup tool
    //Based on the planeclassification is changes colors for all of the added planes
    //For the window it calls the placeobject script
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
            if(item.classification == planeClassification[4])
            {
                Planes.Add(item);
                PlaceObject(item);
            }
            if (item.classification == planeClassification[5])
            {
                Planes.Add(item);
                PlaceObject(item);
            }
        }
    }

    //Function that gets called once a window has been found in the setupplanes function, if placeprefab bool is true
    //Finds the center of the linked plane and gets the rotation of the object, then changes the position and rotation of the linked gameobject to the gathered info
    private void PlaceObject(ARPlane plane)
    {
        if(placePrefab)
        {
            index = Random.Range(0, objectToPlace.Length);

            Vector3 planeCenter = plane.center;
            Quaternion planeRotation = plane.transform.rotation;

            Instantiate(objectToPlace[index], planeCenter, planeRotation);
        }
    }
}
