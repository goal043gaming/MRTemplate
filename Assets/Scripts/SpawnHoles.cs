using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

public class SpawnHoles : MonoBehaviour
{
    [SerializeField] GameObject[] prefabToSpawn;
    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] ARAnchorManager anchorManager;
    [SerializeField] Collider exclusionCollider; 
    [SerializeField] int prefabAmount = 4;

    private int amountUsed = 0;
    private int index = 0;
    private ObjectHandler objHandler;
    [SerializeField] QuestionManager questionManager;

    [Header("UI")]
    [SerializeField] TMP_Text uiNumber;
    [SerializeField] TMP_Text uiAss;
    [SerializeField] GameObject UI;

    private RotateOnEnable rotate;

    void Start()
    {
        rayInteractor.selectEntered.AddListener(SpawnPrefab);
        //rotate = prefabToSpawn[6].GetComponent<RotateOnEnable>();
    }

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

    bool IsInsideCollider(Vector3 point, Collider collider)
    {
        return collider.bounds.Contains(point);
    }

    private void UpdateUI()
    {
        objHandler = prefabToSpawn[index].GetComponent<ObjectHandler>();
        uiAss.text = objHandler.ObjectDescription;
        uiNumber.text = "Objecten om te plaatsen:" + index;
    }
}
