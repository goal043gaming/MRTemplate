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
