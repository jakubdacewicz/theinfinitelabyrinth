using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Room currentRoom;

    public float cameraMoveSpeed;
    private float tempCameraMoveSpeed;
    public float tempCameraSpeedBoost;

    public float smoothing;

    public float acceptedDistBetweenPlayerNCamera;

    private GameObject player;

    private bool isLocked = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tempCameraMoveSpeed = cameraMoveSpeed;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            tempCameraMoveSpeed += 2;
        }

        if (isLocked)
        {
            return;
        }

        UpdatePosition();
    }

    private void UpdatePosition()
    {      
        if (Vector3.Distance(new Vector3(instance.transform.position.x, 0, instance.transform.position.z),
            new Vector3(player.transform.position.x, 0, player.transform.position.z)) < acceptedDistBetweenPlayerNCamera)
        {
            cameraMoveSpeed = tempCameraMoveSpeed;
        }
        else
        {
            cameraMoveSpeed = tempCameraMoveSpeed + tempCameraSpeedBoost;
        }

        if (currentRoom == null)
        {
            cameraMoveSpeed = tempCameraMoveSpeed;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);
    }

    private Vector3 GetCameraTargetPosition()
    {
        if (GameObject.FindWithTag("Player") == null)
        {
            return instance.transform.position;
        }

        Vector3 targetPosition = Vector3.Lerp(transform.position, GameObject.FindWithTag("Player").transform.position - new Vector3(0, 0, 0.21f), smoothing);
        targetPosition.y = 1.5f;

        return targetPosition;
    }

    public void ShakeCamera()
    {
        isLocked = true;

        Vector3 targetPosition = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);

        isLocked = false;
    }
}
