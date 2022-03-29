using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;  

    string currentWorldName = "Forrest";
    private RoomInfo currentRoomData;
    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();
    public List<Room> loadedRooms = new List<Room>();

    private bool isLoadinRoom = false;

    private void Awake()
    {
        instance = this;
    }

    public bool doesRoomExist( int x, int y)
    {
        return loadedRooms.Find( item => item.x == x && item.y == y) != null;
    }
}
