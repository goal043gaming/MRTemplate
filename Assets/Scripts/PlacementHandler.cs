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
    private int selectedObjectNumber;
    [SerializeField] TMP_Text textField;

    [SerializeField] int spheresToPlace;
    [SerializeField] int cubesToPlace;
    [SerializeField] int cylindersToPlace;

    [SerializeField] string objectName1;
    [SerializeField] string objectName2;
    [SerializeField] string objectName3;

    [SerializeField] XRRayInteractor lInteractor;
    [SerializeField] XRRayInteractor rInteractor;

    [SerializeField] ARAnchorManager anchorManager;

    [Header("Debug Settings")]
    public bool testingText;
    [SerializeField] GameObject prevObject;


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
        //ObjectGrabbed();
        if(testingText)
        {
            textField.text = "Amount to place =" + spheresToPlace;
        }

        ShowPreview();
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
                EnablePlacement();
                break;
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
            if(amountToPlace <= 0)
            {
                //allowPlacement = false;
                textField.text = "You've placed all of the objects";
            }

            else
            {
                lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

                Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));
                anchorManager.AddAnchor(hitpose);

                Instantiate(spawnPrefab, hitpose.position, hitpose.rotation);

                UpdateNumber();
            }
        }
    }

    private void CheckObject()
    {
        if(currentObject.name == objectName1)
        {
            amountToPlace = spheresToPlace;
            spawnPrefab = placeObjects[0];

            selectedObjectNumber = 0;

            textField.text = "Objects to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName2)
        {
            amountToPlace = cubesToPlace;
            spawnPrefab = placeObjects[1];

            selectedObjectNumber = 1;

            textField.text = "Objects to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName3)
        {
            amountToPlace = cylindersToPlace;
            spawnPrefab = placeObjects[2];

            selectedObjectNumber = 2;

            textField.text = "Objects to place: " + amountToPlace;
        }
    }

    private void EnablePlacement()
    {
        lInteractor.gameObject.SetActive(true);
        allowPlacement = true;
    }

    public void DroppedObject()
    {
        textField.text = "No Object selected";
        lInteractor.gameObject.SetActive(false);
        allowPlacement = false;
    }

    private void UpdateNumber()
    {
        if(selectedObjectNumber == 0)
        {
            spheresToPlace--;
            amountToPlace--;
        }
        if (selectedObjectNumber == 1)
        {
            cubesToPlace--;
            amountToPlace--;
        }
        if (selectedObjectNumber == 2)
        {
            cylindersToPlace--;
            amountToPlace--;
        }

        textField.text = "Objects to place: " + amountToPlace;
    }

    private void ShowPreview()
    {
        if (spawnPrefab != null && allowPlacement)
        {
            lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

            Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));
            anchorManager.AddAnchor(hitpose);

            spawnPrefab.transform.position = hitpose.position;
            spawnPrefab.transform.rotation = hitpose.rotation;

        }
        if(amountToPlace <= 0)
        {
            allowPlacement = false;
        }
       
    }
}
