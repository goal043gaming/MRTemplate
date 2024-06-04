using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class PlacementHandler : MonoBehaviour
{
    //List of Gameobjects that is used in order to check what object is currently being grabbed
    [SerializeField] List<GameObject> objectsToPlace;

    //Array of Gameobjects that need to be placed, are tied to the grabbed objects
    [SerializeField] GameObject[] placeObjects;
    
    //Gameobject that is currently grabbed by the player, gets changed in objectgrabbed function
    private GameObject currentObject;

    //Gameobject that is tied to the placeobjects, gets changed based on the current held object
    private GameObject spawnPrefab;

    //Int for the amount of items that need to be placed
    private int amountToPlace;

    //Linked Scripts to check what object is currently grabbed and information about the object that is held, like name and general information
    private GrabDetection grabDetection;
    private ObjectHandler objectHandler;

    //Bool to prevent the player from placing down objects when they should'nt, changes on grabbing and dropping
    private bool allowPlacement = false;

    //Int to identify the currently grabbed object
    private int selectedObjectNumber;

    //Int to identify what the total amount is before the process needs to start
    private int totalAmount;

    [Header("UI")]
    //Text field used to display information about the current object, taken from objecthandler
    [SerializeField] TMP_Text objAmount;
    [SerializeField] TMP_Text objName;
    [SerializeField] TMP_Text objDesc;

    //UI Gameobjects that get disabled once the builder tool is completed, after totalamount reaches 0
    [SerializeField] GameObject builderAmount, builderName, builderDesc;

    //Ints to determine the amount of objects that need to be placed for the specific items in objectstoplace
    [SerializeField] int spheresToPlace;
    [SerializeField] int cubesToPlace;
    [SerializeField] int cylindersToPlace;
    [SerializeField] int plantsToPlace;
    [SerializeField] int bucketsToPlace;
    [SerializeField] int targetBToPlace;
    [SerializeField] int targetCToPlace;
    [SerializeField] int targetDToPlace;

    //Strings to tie the grabbed objects to the placeobjects
    [SerializeField] string objectName1;
    [SerializeField] string objectName2;
    [SerializeField] string objectName3;
    [SerializeField] string objectName4;
    [SerializeField] string objectName5;
    [SerializeField] string objectName6;
    [SerializeField] string objectName7;
    [SerializeField] string objectName8;

    //Ray Interactors in order to enable the ray or allow them to place and give haptic feedback
    [SerializeField] XRRayInteractor lInteractor;
    [SerializeField] XRRayInteractor rInteractor;

    //AnchorManager script used in order to place ARAnchor where the objects need to be instantiated
    [SerializeField] ARAnchorManager anchorManager;

    //Process script that is linked in order to start the process once the builder has been completed
    [SerializeField] Process process;

    [Header("Sounds")]
    //Sound that plays once an object is placed
    [SerializeField] AudioSource placementSound;

    [Header("Objectives")]
    //ObjectiveHandler script that is used in order to update the current objective based on whether an object has been grabbed or placed
    [SerializeField] ObjectiveHandler objective;

    //Array of strings to use as objectives
    [SerializeField] string[] objectivesToDisplay;

    //Debug settings used in order to test functionalities without the VR headset
    [Header("Debug Settings")]
    public bool testingText;
    public bool testingProcess;
    [SerializeField] GameObject prevObject;
    public bool testObjects;

    [Header("Haptic Feedback")]
    //Floats used in order to give haptic feedback through the controllers
    [SerializeField][Range(0,1)] float hapIntensity;
    [SerializeField] float hapDuration;

    //Gets all of the attached scripts in the linked objects, starts eventlistener, updates the current objective and determines what the totalamount is
    void Start()
    {
        for(int i = 0; i < objectsToPlace.Count; i++)
        {
           grabDetection = objectsToPlace[i].GetComponent<GrabDetection>();
           objectHandler = objectsToPlace[i].GetComponent<ObjectHandler>();
        }
        lInteractor.selectEntered.AddListener(Spawn);

        objective.UpdateText(objectivesToDisplay[0]);

        totalAmount = plantsToPlace;
    }

    void Update()
    {
        if(testingText)
        {
            objAmount.text = "Amount to place =" + spheresToPlace;
        }
        if(testingProcess)
        {
            totalAmount = 0;
            CheckAmount();
        }

        //If the player can place objects call the showpreview function in order to display what they are currently placing
        if(allowPlacement)
        {
            ShowPreview();
        } 

        if(testObjects)
        {
            Instantiate(placeObjects[3]);
            placeObjects[3].SetActive(false);
            process.FindObjects();
            testObjects = false;
            process.DisplayStep(0);
        }
    }

    //Function that is called by the XR Grab Interactable event in order to determine what object is currently grabbed
    //Checks all of the objectstoplace and gets the grabdetection script in order to check what object is grabbed by the player
    //Once the object has been found it calls the functions checkobject, enableplacement and update the UI
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

    //Function that spawns the objects once allowplacement is true
    //It gets a raycast from the left ray interactor and creates a Pose where the player is aiming and selecting
    //Plays a sound and spawns the current prefab based on the selected object and calls the functions updatenumber and checkamount
    //process findObjects is called for prototype functionality in order to link the process to the instantiated miniplant
    private void Spawn(BaseInteractionEventArgs args)
    {
        if(allowPlacement)
        {
           lInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

           Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(hit.normal));
           anchorManager.AddAnchor(hitpose);

           placementSound.Play();
           Instantiate(spawnPrefab, hitpose.position, hitpose.rotation);

           placeObjects[3].SetActive(false);
           process.FindObjects();
           
           UpdateNumber();
           CheckAmount();
        }
    }

    //Function that checks the currently selected item to see with what name it aligns, based on this is changes the amount to place, what to place, the number identifier and the textfield
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
        else if (currentObject.name == objectName5)
        {
            amountToPlace = bucketsToPlace;
            spawnPrefab = placeObjects[4];

            selectedObjectNumber = 4;

            objAmount.text = "Object to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName6)
        {
            amountToPlace = targetBToPlace;
            spawnPrefab = placeObjects[5];

            selectedObjectNumber = 5;

            objAmount.text = "Object to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName7)
        {
            amountToPlace = targetCToPlace;
            spawnPrefab = placeObjects[6];

            selectedObjectNumber = 6;

            objAmount.text = "Object to place: " + amountToPlace;
        }
        else if (currentObject.name == objectName8)
        {
            amountToPlace = targetDToPlace;
            spawnPrefab = placeObjects[7];

            selectedObjectNumber = 7;

            objAmount.text = "Object to place: " + amountToPlace;
        }
    }

    //Function that gets called on grabbing an object, enables the ray interactor, updates haptic feedback on the controller and updates the objectives, also enables allowplacement so players can spawn objects
    private void EnablePlacement()
    {
        lInteractor.gameObject.SetActive(true);
        allowPlacement = true;
        TriggerHaptic(lInteractor.xrController);

        if(!process.isActive)
        {
            objective.UpdateText(objectivesToDisplay[1]);
        }  
    }

    //Function that gets called by XR Grab Interactable event once on select exited has ben triggered
    //Displays in UI that no object has been selected, disables the ray interactor and disallows placement
    public void DroppedObject()
    {
        objAmount.text = "No Object selected";
        objDesc.text = "No Object selected";
        objName.text = "No Object selected";

        lInteractor.gameObject.SetActive(false);
        allowPlacement = false;


        if (!process.isActive)
        {
            objective.UpdateText(objectivesToDisplay[0]);
        }
    }

    //Function that uses the current number from checkobjects in order to determine the number of items left to place and the total amount, also updated the UI
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
        if (selectedObjectNumber == 4)
        {
            bucketsToPlace--;
            amountToPlace--;
        }
        if (selectedObjectNumber == 5)
        {
            targetBToPlace--;
            amountToPlace--;
        }
        if (selectedObjectNumber == 6)
        {
            targetCToPlace--;
            amountToPlace--;
        }
        if (selectedObjectNumber == 7)
        {
            targetDToPlace--;
            amountToPlace--;
        }

        totalAmount--;
        objAmount.text = "Objects to place: " + amountToPlace;
    }

    //Function that is called if the linked spawnprefab is not null and the allowplacement is true, gets called in the update function
    //Rather than instantiating this function changes the spawnprefab position and rotation to follow the ray interactor of the player
    //Once the player has no objects left to place, allowplacement gets disabled, UI gets updated and the spawnprefab is disabled
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

    //Function that gets called by Objectgrabbed
    //Function gets information from the ObjectHandler script and updates the UI based on the information collected
    private void UpdateUI()
    {
        objectHandler = currentObject.GetComponent<ObjectHandler>();
        objName.text = objectHandler.ObjectName;
        objDesc.text = objectHandler.ObjectDescription;
    }

    //Function that gets called on enable placement, uses the linked controller and sends a pulse to it
    //The pulse is based on the declared floats hapIntensity and HapDuration
    public void TriggerHaptic(XRBaseController controller)
    {
        if(hapIntensity > 0)
        {
            controller.SendHapticImpulse(hapIntensity, hapDuration);
        }
    }

    //Function that gets called by the spawn script, once totalamount is 0 the linked UI objects get disabled and the process gets started
    private void CheckAmount()
    {
        if(totalAmount == 0)
        {
            process.DisplayStep(0);

            builderAmount.SetActive(false);
            builderDesc.SetActive(false);
            builderName.SetActive(false);
        }
    }
}
