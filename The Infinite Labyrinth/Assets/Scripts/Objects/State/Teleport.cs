using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    public float distanceOfNextTeleport;
    public float teleportedPositionShift = 0.2f;

    private RoomController roomController;

    private void Update()
    {
        roomController = GetComponentInParent<RoomController>();
        Debug.DrawRay(transform.position, transform.forward * distanceOfNextTeleport, Color.green);
    }

    public override void Interact()
    {
        if (roomController.currentRoom.GetEnemysAmmount() > 0)
        {
            return;
        }

        RaycastHit hit;

        var directionOfTeleportCheck = new Dictionary<string, string>
        {
            {"TopDoor", "Bottom" },
            {"BottomDoor", "Top" },
            {"LeftDoor", "Right" },
            {"RightDoor", "Left" }
        };

        string acceptedDirectionOfPortal = directionOfTeleportCheck[gameObject.transform.name];

        int layerMask = LayerMask.GetMask(acceptedDirectionOfPortal.Replace("Door", "") + "Portal");

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit , distanceOfNextTeleport, layerMask))
        {
            Vector3 teleportedPosition = hit.transform.position + (hit.transform.forward * -1) * teleportedPositionShift;
            TeleportToCoordinates(GameObject.FindWithTag("Player"), teleportedPosition.x, teleportedPosition.z);

            CameraController cameraControll = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
            cameraControll.LockCamera(false);
            StartCoroutine(cameraControll.SpeedUpCameraForTime(0.3f));           
        }
    }

    public void TeleportToCoordinates(GameObject teleportedObject, float x, float z)
    {
        teleportedObject.transform.position = new Vector3(x, 0.7f, z);
    }
}
