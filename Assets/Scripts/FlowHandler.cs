using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FlowHandler : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Transform altLoc;

    [SerializeField] GameObject[] checkPoints;
    private FlowCheckpoints checkPassed;
    [SerializeField] float speed;

    public bool valve1 = false;
    public bool valve2 = false;

    private int curCheckPoint = 0;
    private Vector3 currentDirection;

    private void Start()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPassed = checkPoints[i].GetComponent<FlowCheckpoints>();
        }
    }
    private void Update()
    {
        SetDirection();

        Move();

        print(checkPassed.checkPointPassed);
    }

    private void SetDirection()
    {
        Vector3 defaultDir = targetPos.position - transform.position;

        if(!valve1 && !valve2)
        {
            currentDirection = defaultDir;
        }
        else if(valve1 && checkPassed.checkPointPassed == true)
        {
            currentDirection = altLoc.position - transform.position;
        }
    }

    public void Move()
    {
        transform.position += currentDirection * speed * Time.deltaTime;
    }
}
