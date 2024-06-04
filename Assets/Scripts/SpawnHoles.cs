using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class SpawnHoles : MonoBehaviour
{
    //Array of Gameobject that are linked in the inspector and get used in the Spawnprefab function
    [SerializeField] GameObject[] prefabToSpawn;

    //Ray interactor that gets used in spawnprefab in order to determine the location of the object spawns
    [SerializeField] XRRayInteractor rayInteractor;

    //AnchorManager script used in order to add anchors to the scene in the spawnprefab function
    [SerializeField] ARAnchorManager anchorManager;

    //Collider used in spawnprefab in order to prevent objects from spawning on and near this collider
    [SerializeField] Collider exclusionCollider;
    
    //Int for the amount of prefabs that need to be created
    [SerializeField] int prefabAmount = 4;

    //Int used to check what the amount of used items is and prevent from placing more once it has reached the prefabamount
    private int amountUsed = 0;

    //Int used to track what the current object is from the array prefabtospawn
    private int index = 0;

    //ObjectHandler script used in order to get the amount and attached text, used in the UpdateUi function
    private ObjectHandler objHandler;

    //QuestionManager script used to generate the questions once all objects have been placed
    [SerializeField] QuestionManager questionManager;

    [Header("UI")]
    //Text and UI fields to enable and update based on the current object, used in UpdateUI
    [SerializeField] TMP_Text uiNumber;
    [SerializeField] TMP_Text uiAss;
    [SerializeField] GameObject UI;

    //RotateOnEnable script that gets used in the SpawnPrefab function
    [SerializeField] RotateOnEnable rotate;

    //Adds eventlistener to the linked ray interactor for calling the Spawnprefab function
    void Start()
    {
        rayInteractor.selectEntered.AddListener(SpawnPrefab);
    }

    //Function that gets called by the select entered event from the linked rayinteractor
    //If the amount of used prefab is less than the total is gets a raycast from the rayinteractor and creates a pose on the location
    //If the location is not outside the collider it adds an ARanchor to the selected location and uses the currently selected prefab and changes its location to the pose
    //Once the amount used is more than the prefabamount it disables the ray and calls the GenerateQuestion on the questionmanager
    void SpawnPrefab(BaseInteractionEventArgs args)
    {
        if (amountUsed < prefabAmount)
        {
            rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);
            Pose hitPose = new Pose(hit.point, Quaternion.LookRotation(hit.normal));

            if (!IsInsideCollider(hitPose.position, exclusionCollider))
            {
                var result = anchorManager.AddAnchor(hitPose);

                UpdateUI();
                prefabToSpawn[index].SetActive(true);
                prefabToSpawn[index].transform.position = hitPose.position;
                prefabToSpawn[index].transform.rotation = hitPose.rotation;
                index++;
                amountUsed++;

                rotate.RotateObject();
            }
            else
            {
                Debug.Log("Cannot spawn within the exclusion collider.");
            }
        }

        if (amountUsed >= prefabAmount)
        {
            rayInteractor.gameObject.SetActive(false);
            if (questionManager == null)
            {
                Debug.Log("Missing Question Manager");
            }
            else
            {
                questionManager.GenerateQuestion();
                UI.SetActive(false);
            }
        }
    }

    //Variable/Function that gets called by the spawnprefab function to check if the spawnposition is inside the collider
    bool IsInsideCollider(Vector3 point, Collider collider)
    {
        return collider.bounds.Contains(point);
    }

    //Function that gets called in the Spawnprefab function to update the UI based on the current object
    private void UpdateUI()
    {
        objHandler = prefabToSpawn[index].GetComponent<ObjectHandler>();
        uiAss.text = objHandler.ObjectDescription;
        uiNumber.text = "Objecten om te plaatsen:" + index;
    }
}
