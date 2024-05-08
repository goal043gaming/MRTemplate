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
    private ObjectHandler objectHandler;

    private bool allowPlacement = false;
    private int selectedObjectNumber;

    [SerializeField] TMP_Text objAmount;
    [SerializeField] TMP_Text objName;
    [SerializeField] TMP_Text objDesc;

    [SerializeField] int spheresToPlace;
    [SerializeField] int cubesToPlace;
    [SerializeField] int cylindersToPlace;
    [SerializeField] int plantsToPlace;

    [SerializeField] string objectName1;
    [SerializeField] string objectName2;
    [SerializeField] string objectName3;
    [SerializeField] string objectName4;

    [SerializeField] XRRayInteractor lInteractor;
    [SerializeField] XRRayInteractor rInteractor;

    [SerializeField] ARAnchorManager anchorManager;

    [Header("Sounds")]
    [SerializeField] AudioSource placementSound;

    [Header("Objectives")]
    [SerializeField] ObjectiveHandler objective;
    [SerializeField] string[] objectivesToDisplay;

    [Header("Debug Settings")]
    public bool testingText;
    [SerializeField] GameObject prevObject;

    [Header("Haptic Feedback")]
    [SerializeField][Range(0,1)] float hapIntensity;
    [SerializeField] float hapDuration;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < objectsToPlace.Count; i++)
        {
           grabDetection = objectsToPlace[i].GetComponent<GrabDetection>();
           objectHandler = objectsToPlace[i].GetComponent<ObjectHandler>();
        }
        lInteractor.selectEntered.AddListener(Spawn);

        objective.UpdateText(objectivesToDisplay[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if(testingText)
        {
            objAmount.text = "Amount to place =" + spheresToPlace;
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
                UpdateUI();

                break;
            }
            else
            {
                objAmount.text = "No Object selected";
            }
        }

    }

    private void Spawn(BaseInteractionEventArgs args)
    {
        if(allowPlacement)
        {
           lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

           Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(hit.normal));
           anchorManager.AddAnchor(hitpose);

           placementSound.Play();
           Instantiate(spawnPrefab, hitpose.position, hitpose.rotation);

           UpdateNumber();
        }
    }

    private void CheckObject()
    {
        if(currentObject.name == objectName1)
        {
            amountToPlace = spheresToPlace;
            spawnPrefab = placeObjects[0];

            selectedObjectNumber = 0;

            objAmount.text = "Objects to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName2)
        {
            amountToPlace = cubesToPlace;
            spawnPrefab = placeObjects[1];

            selectedObjectNumber = 1;

            objAmount.text = "Objects to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName3)
        {
            amountToPlace = cylindersToPlace;
            spawnPrefab = placeObjects[2];

            selectedObjectNumber = 2;

            objAmount.text = "Objects to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName4)
        {
            amountToPlace = plantsToPlace;
            spawnPrefab = placeObjects[3];

            selectedObjectNumber = 3;

            objAmount.text = "Object to place: " + amountToPlace;
        }
    }

    private void EnablePlacement()
    {
        lInteractor.gameObject.SetActive(true);
        allowPlacement = true;
        TriggerHaptic(lInteractor.xrController);

        objective.UpdateText(objectivesToDisplay[1]);
    }

    public void DroppedObject()
    {
        objAmount.text = "No Object selected";
        objDesc.text = "No Object selected";
        objName.text = "No Object selected";

        lInteractor.gameObject.SetActive(false);
        allowPlacement = false;

        objective.UpdateText(objectivesToDisplay[0]);
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
        if (selectedObjectNumber == 3)
        {
            plantsToPlace--;
            amountToPlace--;
        }

        objAmount.text = "Objects to place: " + amountToPlace;
    }

    private void ShowPreview()
    {
        if (spawnPrefab != null && allowPlacement)
        {
            lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

            Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(hit.normal));
            anchorManager.AddAnchor(hitpose);

            spawnPrefab.transform.position = hitpose.position;
            spawnPrefab.transform.rotation = hitpose.rotation;

        }
        if(amountToPlace <= 0)
        {
            allowPlacement = false;
            objAmount.text = "You've placed all of the objects";
            spawnPrefab.SetActive(false);
        }
       
    }

    private void UpdateUI()
    {
        objectHandler = currentObject.GetComponent<ObjectHandler>();
        objName.text = objectHandler.ObjectName;
        objDesc.text = objectHandler.ObjectDescription;
    }

    public void TriggerHaptic(XRBaseController controller)
    {
        if(hapIntensity > 0)
        {
            controller.SendHapticImpulse(hapIntensity, hapDuration);
        }
    }
}
