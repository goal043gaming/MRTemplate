using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    // Start is called before the first frame update
    FakeCam cam;
    void Start()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z));
        cam = GetComponentInChildren<FakeCam>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.DoorTransform = transform;
    }
}
