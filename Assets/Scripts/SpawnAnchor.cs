using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class SpawnAnchor : MonoBehaviour
{
    [Header("Prefab to spawn")]
    public GameObject spawnPrefab;

    [Header("Objects to link")]
    public XRRayInteractor rayInteractor;
    public ARAnchorManager anchorManager;

    private bool used = false;

    public ARPlaneManager planeManager;
    public PlaneClassification planeClassification;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.selectEntered.AddListener(Spawn);

      /*  foreach(var plane in planeManager.trackables)
        {
            if(plane.classification == planeClassification)
            {
                Renderer rend = GetComponent<Renderer>();
                rend.material.color = Color.green;
            }
            else
            {
                Renderer itemRenderer = plane.GetComponent<Renderer>();
                Destroy(itemRenderer);
            }
        } */
    }

    public void Spawn(BaseInteractionEventArgs args)
    {
        if (!used)
        {
            rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

            Pose hitPose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));

            var result = anchorManager.AddAnchor(hitPose);

            GameObject SpawnedPrefab = Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
            SpawnedPrefab.transform.parent = result.transform;
            used = true;
        }
        else
        {
            print("Already placed");
        }
    }
}
