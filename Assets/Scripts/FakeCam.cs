using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCam : MonoBehaviour
{
    public float distance = 30f;
    [SerializeField] GameObject sceneCamera;
    //[SerializeField] GameObject xrOrigin;
    [SerializeField] Camera mainCamera;
    //private Camera mainCamera;
    public Transform DoorTransform { get; set; } = null;

    private void Awake()
    {
        //mainCamera = xrOrigin.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorTransform == null)
        {
            return;
        }

        Vector3 directionToCamera = mainCamera.transform.position - DoorTransform.position;
        Vector3 sceneCameraPosition = DoorTransform.position + directionToCamera.normalized * distance;

        sceneCameraPosition.y = sceneCameraPosition.y > 2f ? 2f : sceneCameraPosition.y;

        sceneCamera.transform.position = sceneCameraPosition;
        sceneCamera.transform.rotation = Quaternion.LookRotation(DoorTransform.position - sceneCameraPosition);
    }
}
