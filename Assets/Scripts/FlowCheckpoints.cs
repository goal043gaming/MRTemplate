using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowCheckpoints : MonoBehaviour
{
    public bool checkPointPassed = false;
    public bool isActive;

    public bool valveOpen = false;

    [SerializeField] public Transform rightDirection;
    [SerializeField] public Transform wrongDirection;

    [SerializeField][Range(0,1)] float disableTimer;

    [SerializeField] FlowHandler flowHandler;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Flow")
        {
            checkPointPassed = true;
            StartCoroutine(disableCheckPoint());
        }
        else
        {
            checkPointPassed = false;
        }   
    }

    private IEnumerator disableCheckPoint()
    {
        yield return new WaitForSeconds(disableTimer);
        checkPointPassed = false;
    }
}
