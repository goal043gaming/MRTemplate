using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class PlacementHandler : MonoBehaviour
{

    [SerializeField] List<GameObject> objectsToPlace;
    [SerializeField] GameObject[] placeObjects;
    
    private GameObject currentObject;
    private GameObject spawnPrefab;

    private int amountToPlace;
    private GrabDetection grabDetection;
    private bool allowPlacement = false;
    [SerializeField] TMP_Text textField;

    [SerializeField] int spheresToPlace;
    [SerializeField] int cubesToPlace;
    [SerializeField] int cylindersToPlace;

    [SerializeField] XRRayInteractor lInteractor;
    [SerializeField] XRRayInteractor rInteractor;

    [SerializeField] ARAnchorManager anchorManager;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < objectsToPlace.Count; i++)
        {
           grabDetection = objectsToPlace[i].GetComponent<GrabDetection>();
        }
        lInteractor.selectEntered.AddListener(Spawn);
    }

    // Update is called once per frame
    void Update()
    {
        ObjectGrabbed();
    }

    public void ObjectGrabbed()
    {
        for (int i = 0; i < objectsToPlace.Count; i++)
        {
            grabDetection = objectsToPlace[i].GetComponent<GrabDetection>();
            
            if(grabDetection.isGrabbed)
            {
                currentObject = objectsToPlace[i];             

                CheckObject();
                Debug.Log("DEBUG: Testing 1");
                EnablePlacement();
                Debug.Log("DEBUG: Testing 2");
            }
            else
            {
                textField.text = "No Object selected";
            }
        }
    }

    private void Spawn(BaseInteractionEventArgs args)
    {
        if(allowPlacement)
        {
           Debug.Log("DEBUG: Testing 4");
           lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

           Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));
           anchorManager.AddAnchor(hitpose);

           Instantiate(spawnPrefab, hitpose.position, hitpose.rotation);
           Debug.Log("DEBUG: Testing 5");
        }
    }

    private void CheckObject()
    {
        if(currentObject.name == "Cube")
        {
            amountToPlace = cubesToPlace;
            spawnPrefab = placeObjects[0];
        }
        if (currentObject.name == "Sphere")
        {
            amountToPlace = spheresToPlace;
            spawnPrefab = placeObjects[1];
        }
        if (currentObject.name == "Cylinder")
        {
            amountToPlace = cylindersToPlace;
            spawnPrefab = placeObjects[2];
        }

        textField.text = "Objects to place:" + amountToPlace.ToString();

    }

    private void EnablePlacement()
    {
        lInteractor.gameObject.SetActive(true);
        allowPlacement = true;
    }
}
