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

    public float smoothTime;

    private Vector3 velocity = Vector3.zero;

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

    private void Update()
    {
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

        if (GameObject.FindWithTag("Player") == null)
        {
            transform.position = instance.transform.position;

            return;
        }

        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, player.transform.position - new Vector3(0, 0, 0.21f), ref velocity, smoothTime);
        targetPosition.y = 1.5f;

        transform.position = targetPosition;
    }

    public void ShakeCamera()
    {
        isLocked = true;

        Vector3 targetPosition = transform.position + new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);

        isLocked = false;
    }
}
