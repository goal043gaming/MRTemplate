using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;

public class SpawnAnchor : MonoBehaviour
{
    public GameObject spawnPrefab;

    public XRRayInteractor rayInteractor;
    public ARAnchorManager anchorManager;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.selectEntered.AddListener(Spawn);
    }

    public void Spawn(BaseInteractionEventArgs args)
    {
        rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        Pose hitPose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));

        var result = anchorManager.AddAnchor(hitPose);

        GameObject SpawnedPrefab = Instantiate(spawnPrefab, hitPose.position, hitPose.rotation);
        SpawnedPrefab.transform.parent = result.transform;
    }
}
