using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FlowHandler : MonoBehaviour
{
    [SerializeField] Transform targetPos;
    [SerializeField] Transform altLoc;

    [SerializeField] GameObject[] checkPoints;
    [SerializeField] GameObject[] objectsToMove;

    private GameObject currentObjectToMove;

    private FlowCheckpoints checkPassed;

    [SerializeField] float speed;

    public bool v1isOpen = false;
    private bool used = false;

    private int curCheckPoint = 0;
    private Vector3 currentDirection;

    private bool allowMovement = false;

    private void Start()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPassed = checkPoints[i].GetComponent<FlowCheckpoints>();
        }

        allowMovement = true;
        currentObjectToMove = objectsToMove[0];
    }
    private void Update()
    {
        SetDirection();

        Move();

        print(checkPassed.checkPointPassed);
    }

    private void SetDirection()
    {
        Vector3 defaultDir = targetPos.position - currentObjectToMove.transform.position;

        if(!v1isOpen)
        {
            currentDirection = defaultDir;
        }
        else if(v1isOpen && checkPassed.checkPointPassed == true)
        {
            currentDirection = altLoc.position - currentObjectToMove.transform.position;
            allowMovement = false;
            objectsToMove[1].SetActive(true);
            currentObjectToMove = objectsToMove[1];
            allowMovement = true;
        }
    }

    public void Move()
    {
        if(allowMovement)
        {
            currentObjectToMove.transform.position += currentDirection * speed / 2 * Time.deltaTime;
            currentObjectToMove.transform.localScale += currentDirection * speed * Time.deltaTime;
        }
    }
}
