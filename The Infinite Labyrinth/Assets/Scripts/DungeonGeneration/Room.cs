using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Room : MonoBehaviour
{
    public float width;
    public float height;
    public float length;

    public int X;
    public int Z;

    public int enemysAmmount = 0;

    private bool updatedDoors;

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
                    if (GetRight() == null)
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
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);

            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy") && child.GetComponent<EnemyController>().enabled == true)
                {
                    child.gameObject.SetActive(true);
                    enemysAmmount++;
                }

                if (child.CompareTag("Trap"))
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Enemy") || child.CompareTag("Trap"))
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetDoorsActive(bool active)
    {
        BoxCollider[] boxColliders = GetComponentsInChildren<BoxCollider>();
        boxColliders = boxColliders.Where(child => child.tag == "Portal").ToArray();

        foreach (BoxCollider boxCollider in boxColliders)
        {
            boxCollider.enabled = active;
        }
    }

     public void DecreaseEnemyAmmount()
    {
        enemysAmmount--;
    }
}
