using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float width;
    public float height;
    public float length;

    public int x;
    public int y;
    public int z;

    private void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, length));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(x * width, y * height, z * length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
