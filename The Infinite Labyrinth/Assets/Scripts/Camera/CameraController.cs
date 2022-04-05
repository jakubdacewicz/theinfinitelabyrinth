using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Room currentRoom;

    public float cameraMoveSpeed;
    public float tempCameraSpeedBoost;
    public float cameraAfterMoveHeightShift;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if(currentRoom == null)
        {
            return;
        }

        Vector3 targetPosition = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * cameraMoveSpeed);
    }

    private Vector3 GetCameraTargetPosition()
    {
        //if(currentRoom == null)
        //{
        //    return Vector3.zero;
        //}

        if (GameObject.FindWithTag("Player") == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPosition = GameObject.FindWithTag("Player").transform.position - new Vector3(0, 0, 0.5f);

        targetPosition.y = CalculateNewCameraHeight();

        return targetPosition;
    }

    private float CalculateNewCameraHeight()
    {
        return currentRoom.width * currentRoom.length + cameraAfterMoveHeightShift;
    }

    public bool IsSwitchingScene()
    {
        return transform.position.Equals(GetCameraTargetPosition()) == false;
    }

    public IEnumerator SpeedUpCameraForTime(float secounds)
    {
        cameraMoveSpeed += tempCameraSpeedBoost;

        yield return new WaitForSeconds(secounds);

        cameraMoveSpeed -= tempCameraSpeedBoost;
    }
}
