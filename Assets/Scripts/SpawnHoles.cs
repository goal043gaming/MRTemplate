using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnHoles : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;

    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] ARAnchorManager anchorManager;

    [SerializeField] int prefabAmount = 3;
    private int amountUsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        rayInteractor.selectEntered.AddListener(spawnPrefab);
    }

    public void spawnPrefab(BaseInteractionEventArgs args)
    {
        if(amountUsed <= prefabAmount)
        {
            rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit);
            Pose hitpose = new Pose(hit.point, Quaternion.LookRotation(-hit.normal));

            var result = anchorManager.AddAnchor(hitpose);

            Instantiate(prefabToSpawn, hitpose.position, hitpose.rotation);
            amountUsed++;
        }
        else
        {
            
        }
    }
}
