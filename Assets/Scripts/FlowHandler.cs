using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FlowHandler : MonoBehaviour
{
    [SerializeField] Transform[] targets;
    [SerializeField] GameObject[] checkPoints;
    [SerializeField] GameObject[] objectsToMove;

    [SerializeField] Transform startingTarget;
    private GameObject currentObjectToMove;
    private Transform currentTarget;

    private bool isTurnedX = false;
    private bool isTurnedY = false;

    private FlowCheckpoints checkPassed;
    private FlowCheckpoints currentPoint;

    [SerializeField] float speed;

    public bool v1isOpen = false;
    public bool v2isOpen = false;

    private Vector3 currentDirection;

    public bool allowMovement = false;

    private bool check1Passed;

    private bool valveIsOpen = false;

    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    private void Start()
    {
        /* for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPassed = checkPoints[i].GetComponent<FlowCheckpoints>();
        }*/



        //checkPassed = checkPoints[0].GetComponent<FlowCheckpoints>();

        allowMovement = true;
        currentObjectToMove = objectsToMove[0];
        currentTarget = startingTarget;

    }
    private void Update()
    {
        //SetDirection();

        CurrentDirection();

        Move();

        CurrentCheckPoint();
    }

    private void SetDirection()
    {
        Vector3 defaultDir = currentTarget.position - currentObjectToMove.transform.position;

        if(!v1isOpen)
        {
            currentDirection = defaultDir;
        }
        else if(v1isOpen && checkPassed.checkPointPassed == true)
        {
            currentTarget = targets[1];
            objectsToMove[1].SetActive(true);
            currentObjectToMove = objectsToMove[1];

            currentDirection = currentTarget.position - currentObjectToMove.transform.position;

            checkPassed = checkPoints[1].GetComponent<FlowCheckpoints>();

            check1Passed = true;

            if(v2isOpen && checkPassed.checkPointPassed == true && check1Passed == true)
            {
                currentTarget = targets[2];
                objectsToMove[2].SetActive(true);
                currentObjectToMove = objectsToMove[2];

                currentDirection = currentTarget.position - currentObjectToMove.transform.position;
            }
            else if(!v2isOpen && checkPassed.checkPointPassed == true && check1Passed == true) {
                currentTarget = targets[3];
                objectsToMove[2].SetActive(true);
                currentObjectToMove = objectsToMove[2];

                currentDirection = currentTarget.position - currentObjectToMove.transform.position;
            }
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

    private void CurrentCheckPoint()
    {
        for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPassed = checkPoints[i].GetComponent<FlowCheckpoints>();

            if(checkPassed.checkPointPassed)
            {
                currentPoint = checkPoints[i].GetComponent<FlowCheckpoints>();
                valveIsOpen = currentPoint.valveOpen;
                currentTarget = currentPoint.curTarget;
                currentObjectToMove = currentPoint.linkedObject;
                currentObjectToMove.SetActive(true);

                if (currentDirection.x < xOffset)
                {
                    CheckFacingX();
                }
                if(currentDirection.y < yOffset)
                {
                    CheckFacingY();
                }
                
                break;
            }
        }
    }

    private void CurrentDirection()
    {
        currentDirection = currentTarget.position - currentObjectToMove.transform.position;
    }

    private void CheckFacingX()
    {
        if (currentDirection.x < xOffset && !isTurnedX)
        {
            currentObjectToMove.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            isTurnedX = true;
        }
    }

    private void CheckFacingY()
    {
        if(currentDirection.y < yOffset && !isTurnedY)
        {
            currentObjectToMove.transform.localScale = new Vector3(-0.3f, -0.3f, 0.3f);
            isTurnedY = true;
        }
    }
}
