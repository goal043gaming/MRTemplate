using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnHoles : MonoBehaviour
{
    [SerializeField] GameObject[] prefabToSpawn;

    [SerializeField] XRRayInteractor rayInteractor;
    [SerializeField] ARAnchorManager anchorManager;

    [SerializeField] int prefabAmount = 4;
    private int amountUsed = 0;
    private int index = 0;

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

            Instantiate(prefabToSpawn[index], hitpose.position, hitpose.rotation);
            updatePrefab();

            amountUsed++;

        }
        if(amountUsed >= prefabAmount)
        {
            rayInteractor.gameObject.SetActive(false);
        }
    }

    private void updatePrefab()
    {
        index++;
    }
}
