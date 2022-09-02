using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    public float waitTime;
    public float rotationSpeed;

    private GameObject keyObject;
    private GameObject endDoorObject;

    private Vector3 targetPosition;

    private bool isArrowLocked = true;

    private void Start()
    {
        StartCoroutine(FindObjectives());
    }

    private void Update()
    {
        if (isArrowLocked) return;

        if (keyObject != null)
        {
            targetPosition = Camera.main.transform.InverseTransformPoint(keyObject.transform.position);
        }
        else if(endDoorObject != null)
        {
            targetPosition = Camera.main.transform.InverseTransformPoint(endDoorObject.transform.position);
        }

        var targetAngle = -Mathf.Atan2(targetPosition.x, targetPosition.y) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, targetAngle);
    }       
       
    private IEnumerator FindObjectives()
    {
        yield return new WaitForSeconds(waitTime);

        keyObject = GameObject.FindGameObjectWithTag("Key");
        endDoorObject = GameObject.FindGameObjectWithTag("EndDoor");

        isArrowLocked = false;
    }
}
