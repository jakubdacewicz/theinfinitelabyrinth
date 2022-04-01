using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : Interactable
{
    public float distanceOfNextTeleport;
    public float teleportedPositionShift = 0.2f;  

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * distanceOfNextTeleport, Color.green);
    }

    public override void Interact()
    {
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

                TeleportToCoordinates(GameObject.FindWithTag("Player"), teleportedPosition.x, teleportedPosition.y, teleportedPosition.z);
            
            
        }
    }

    public void TeleportToCoordinates(GameObject teleportedObject, float x, float y, float z)
    {
        teleportedObject.transform.position = new Vector3(x, y, z);

        Debug.Log("Teleported " + teleportedObject.name + " to coordinates x: " + x + ", y: " + y + ", z: " + z);
    }
}
