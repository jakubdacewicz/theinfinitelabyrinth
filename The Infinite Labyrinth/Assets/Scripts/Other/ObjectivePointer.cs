using UnityEngine;

public class ObjectivePointer : MonoBehaviour
{
    public float rotationSpeed;

    private GameObject keyObject;
    private GameObject endDoorObject;

    private Vector3 targetPosition;

    private void Start()
    {
        InvokeRepeating(nameof(FindObjectives), 0, 3.0f);
    }

    private void Update()
    {
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
       
    private void FindObjectives()
    {
        keyObject = GameObject.FindGameObjectWithTag("Key");
        endDoorObject = GameObject.FindGameObjectWithTag("EndDoor");
    }
}
