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

    private GameObject currentObjectToMove;
    private Transform currentTarget;

    private FlowCheckpoints checkPassed;

    [SerializeField] float speed;

    public bool v1isOpen = false;
    public bool v2isOpen = false;

    private Vector3 currentDirection;

    private bool allowMovement = false;

    private bool check1Passed;

    private void Start()
    {
        /* for(int i = 0; i < checkPoints.Length; i++)
        {
            checkPassed = checkPoints[i].GetComponent<FlowCheckpoints>();
        }*/

        allowMovement = true;

        checkPassed = checkPoints[0].GetComponent<FlowCheckpoints>();
        currentObjectToMove = objectsToMove[0];
        currentTarget = targets[0];
    }
    private void Update()
    {
        SetDirection();

        Move();

        print(checkPassed.checkPointPassed);
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
}
