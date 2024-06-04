using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    //FakeCam script used to assign variables used in the FakeCam script
    FakeCam cam;

    //Start function which assign the transform to look at the current main camera position and gets the fakecam component
    void Start()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        cam = GetComponentInChildren<FakeCam>();
    }

    //Update function consistently updates the variable inside fakecam to the current transform of this object
    void Update()
    {
        cam.DoorTransform = transform;
    }
}
