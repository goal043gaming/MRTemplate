using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCam : MonoBehaviour
{
    //float for calculations for the fake in scene view
    public float distance = 30f;

    //linked cameras in the scene, maincamera being the camera in the prefab
    [SerializeField] GameObject sceneCamera;
    [SerializeField] Camera mainCamera;

    //The transform of the object, used for calculations
    public Transform DoorTransform { get; set; } = null;

    // Update is called once per frame
    void Update()
    {
        if (DoorTransform == null)
        {
            return;
        }

        //Calculate the vector from the object to the main camera
        Vector3 directionToCamera = mainCamera.transform.position - DoorTransform.position;
        
        //Calculate the position where the camera should be, done by moving units away from the door in the direction of the main camera
        Vector3 sceneCameraPosition = DoorTransform.position + directionToCamera.normalized * distance;

        //Prevents the y-coordinate of the camera exceeding 2 units, should be made into a variable
        sceneCameraPosition.y = sceneCameraPosition.y > 2f ? 2f : sceneCameraPosition.y;

        //Set the camera of the object on the calculated position
        sceneCamera.transform.position = sceneCameraPosition;

        //Rotate the camera to the object
        sceneCamera.transform.rotation = Quaternion.LookRotation(DoorTransform.position - sceneCameraPosition);
    }
}
