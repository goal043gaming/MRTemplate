using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCam : MonoBehaviour
{
    public float distance = 30f;
    [SerializeField] GameObject sceneCamera;
    private Camera mainCamera;
    public Transform DoorTransform { get; set; } = null;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoorTransform == null)
        {
            return;
        }

        // Calculate the direction and position offset from the door to the main camera
        Vector3 directionToCamera = mainCamera.transform.position - DoorTransform.position;
        Vector3 sceneCameraPosition = DoorTransform.position + directionToCamera.normalized * distance;

        sceneCameraPosition.y = sceneCameraPosition.y > 2f ? 2f : sceneCameraPosition.y;

        // Update the position and rotation of the scene camera
        sceneCamera.transform.position = sceneCameraPosition;
        sceneCamera.transform.rotation = Quaternion.LookRotation(DoorTransform.position - sceneCameraPosition);
    }
}
