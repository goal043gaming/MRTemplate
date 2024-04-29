using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ValveRotator : MonoBehaviour
{
    [SerializeField] Transform objectToRotate;
    [SerializeField] int rotationAmount = 25;
    [SerializeField] float angleTolerance;

    private XRBaseInteractor interactor;
    private float startAngle;
    private bool requireStartAngle = true;
    private bool shouldGetHandRotation = false;

    private XRGrabInteractable grabInteractor => GetComponent<XRGrabInteractable>();

    // Start is called before the first frame update
    void Start()
    {
        grabInteractor.selectEntered.AddListener(GrabbedBy);
        grabInteractor.selectExited.AddListener(GrabbedEnd);
    }

    private void GrabbedBy(SelectEnterEventArgs args)
    {
        interactor = GetComponent<XRGrabInteractable>().selectingInteractor;
        interactor.GetComponent<XRDirectInteractor>().hideControllerOnSelect = true;

        shouldGetHandRotation = true;
        startAngle = 0;
    }

    private void GrabbedEnd(SelectExitEventArgs args)
    {
        shouldGetHandRotation = false;
        requireStartAngle = true;
    }

    private void Update()
    {
        if (shouldGetHandRotation)
        {
            var rotationAngle = GetInteractorRotation();
            GetRotationDistance(rotationAngle);
        }
    }

    public float GetInteractorRotation() => interactor.GetComponent<Transform>().eulerAngles.z;

    private void GetRotationDistance(float currentAngle)
    {
        if(!requireStartAngle)
        {
            var angleDifference = Mathf.Abs(startAngle - currentAngle);

            if(angleDifference > angleTolerance)
            {
                if(angleDifference > 270)
                {
                    float angleCheck;

                    if(startAngle < currentAngle)
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);
                        if(angleCheck < angleTolerance)
                        {
                            return;
                        }
                        else
                        {
                            RotateClockWise();
                            startAngle = currentAngle;
                        }
                    }
                    else if (startAngle > currentAngle)
                    {
                        angleCheck = CheckAngle(currentAngle, startAngle);

                        if(angleCheck < angleTolerance)
                        {
                            return;
                        }
                        else
                        {
                            RotateAntiClockWise();
                            startAngle = currentAngle;
                        }
                    }
                }
            }
        }
    }

    private float CheckAngle(float currentAngle, float startAngle) => (360f - currentAngle) + startAngle;

    private void RotateClockWise()
    {
        objectToRotate.localEulerAngles = new Vector3(objectToRotate.localEulerAngles.x, objectToRotate.localEulerAngles.y, objectToRotate.localEulerAngles.z + rotationAmount);
    }

    private void RotateAntiClockWise()
    {
        objectToRotate.localEulerAngles = new Vector3(objectToRotate.localEulerAngles.x, objectToRotate.localEulerAngles.y, objectToRotate.localEulerAngles.z - rotationAmount);
    }
}
