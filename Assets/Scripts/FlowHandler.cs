using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class FlowHandler : MonoBehaviour
{
    //NO LONGER USED
    [SerializeField] Transform[] targets;

    //Attached gameobject who are checkpoints, linked to Flowcheckpoints which communicates aspects like the current object to move and the direction
    [SerializeField] GameObject[] checkPoints;

    //Array is not used in this version of the script, the first object in the array is used in StartFlow
    [SerializeField] GameObject[] objectsToMove;

    //Transform for the first target to which the flow needs to move
    [SerializeField] Transform startingTarget;

    //Gameobject taken from Flowcheck or from objectstomove in order to change the object that needs to move
    private GameObject currentObjectToMove;

    //Transform that gets changed by the Flowcheckpoints script and the currentchecpoint function
    private Transform currentTarget;


    //Booleans to check if a object has already been turned, used to prevent visualisation bug in moving the objects
    private bool isTurnedX = false;
    private bool isTurnedY = false;

    //Flowcheckpoints to communicate with the checkpoints on aspects like distance, object to move or the current direction
    private FlowCheckpoints checkPassed;
    private FlowCheckpoints currentPoint;

    //Float for the speed of the flow, should not be higher than 0.6
    [SerializeField] float speed;

    //NO LONGER USED
    public bool v1isOpen = false;
    public bool v2isOpen = false;

    //Vector3 used to determine the current direction to move in, used in all movement type scripts like: currentcheckpoint, currentdirection, checkturned x & y
    private Vector3 currentDirection;

    //Bool that gets changed by for example flowstop, if disabled the flow will not run
    public bool allowMovement = false;

    //NO LONGER USED
    private bool check1Passed;

    //NO LONGER USED
    private bool valveIsOpen = false;

    //Floats used in order to determine the offset before the Checkfacing X & Y needs to be called
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;

    //Linked flood to check the state and update the current objective based on the state of the flood.
    [SerializeField] Flood flood;

    //Objectivehandler script that changes text in UI element in order to display the current objective
    [SerializeField] ObjectiveHandler objectiveHandler;

    //Array of strings to communicate what the current objective is
    [SerializeField] string[] objectivesToDisplay;

    private void Start()
    {
        //Start the flow, could be started by another object but was needed in the start function for prototyping purposes
        StartFlow();
    }
    
    //Enables the movement and assigns the current object that needs to be moved and into what direction, also assigns the current objective
    public void StartFlow()
    {
        allowMovement = true;
        currentObjectToMove = objectsToMove[0];
        currentTarget = startingTarget;

        objectiveHandler.UpdateText(objectivesToDisplay[0]);
    }
    private void Update()
    {

        //Constantly calls the methods below in order to determine the flow, direction and make it move
        CurrentDirection();
        Move();
        CurrentCheckPoint();


        //Based on these booleans from the flood script the objective changes
        if(flood.flooding)
        {
            objectiveHandler.UpdateText(objectivesToDisplay[1]);
        }

        if(flood.hasFlooded)
        {
            objectiveHandler.UpdateText(objectivesToDisplay[2]);
        }
    }

    //NO LONGER USED
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

    //Move script that only works if the bool allowmovement is on, changes both position and scale in order to simulate a flow
    public void Move()
    {
        if(allowMovement)
        {
            currentObjectToMove.transform.position += currentDirection * speed / 2 * Time.deltaTime;
            currentObjectToMove.transform.localScale += currentDirection * speed * Time.deltaTime;
        }

        
    }

    //Determines the current checkpoint, checks if the valve is open to determine the direction, the object that should be moves and if Checkfacing X & Y should be called.
    //Uses Flowcheckpoints in order to determine all of the required information
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
                isTurnedY = false;
                isTurnedX = false;

                if (currentDirection.x < xOffset)
                {
                    CheckFacingX();
                }
                if(currentDirection.y > yOffset)
                {
                    CheckFacingY();
                }
                
                break;
            }
        }
    }

    //Changes the direction based on the currentcheckpoint check and the flowcheckpoints script
    private void CurrentDirection()
    {
        currentDirection = currentTarget.position - currentObjectToMove.transform.position;
    }

    //Used in order to flip the orientation of the cube to make sure the movement goes as it should
    private void CheckFacingX()
    {
        if (currentDirection.x < xOffset && !isTurnedX)
        {
            currentObjectToMove.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f);
            isTurnedX = true;
        }
    }

    //Used in order to flip the orientation of the cube to make sure the movement goes as it should
    private void CheckFacingY()
    {
        if (currentDirection.y > yOffset && !isTurnedY)
        {
            currentObjectToMove.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            isTurnedY = true;
        }
    }
}
