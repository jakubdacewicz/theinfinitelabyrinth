using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float width;
    public float height;
    public float length;

    public int X;
    public int Z;

    private bool updatedDoors;

    /*
    public Room(int X, int Z)
    {
        this.X = X;
        this.Z = Z;
    }
    */

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors;

    private void Awake()
    {
        doors = new List<Door>();    
    }

    public void SetXandZ(int X, int Z)
    {
        this.X = X;
        this.Z = Z;
    }

    private void Start()
    {
        if (RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene!");
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.type)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if (!updatedDoors)
        {
            if (name.Contains("Shop") || name.Contains("End"))
            {
                StartCoroutine(RemoveUnconectedDoors());
            }
            else if (name.Contains("Key"))
            {
                StartCoroutine(RemoveUnconectedDoors());
                updatedDoors = true;
            }
        }      
    }

    public IEnumerator RemoveUnconectedDoors()
    {
        yield return new WaitForSeconds(0.15f);

        foreach (Door d in doors)
        {
            switch (d.type)
            {
                case Door.DoorType.right:
                    if(GetRight() == null)
                        d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        d.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        d.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Z))
        {
            return RoomController.instance.FindRoom(X + 1, Z);
        }
        return null;
    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Z))
        {
            return RoomController.instance.FindRoom(X - 1, Z);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Z + 1))
        {
            return RoomController.instance.FindRoom(X, Z + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Z - 1))
        {
            return RoomController.instance.FindRoom(X, Z - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, length));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, 0, Z * length);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
