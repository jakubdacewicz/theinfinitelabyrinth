using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int X;
    public int Z;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    public List<Room> loadedRooms = new List<Room>();

    string currentWorldName = "Desert";

    RoomInfo currentLoadRoomData;

    private Room currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    bool isLoadingRoom = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        /*
        LoadRoom("Start", 0, 0);
        LoadRoom("1", 1, 0);
        LoadRoom("2", -1, 0);
        LoadRoom("1", 0, 1);
        LoadRoom("2", 0, -1);
        */
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    private void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        
        if (DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Z))
        {
            return;
        }
        
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name, int x, int z)
    {
        /*
        if (DoesRoomExist(x, z))
        {
            return;
        }
        */

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Z = z;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(
            currentLoadRoomData.X * room.width,
            0,
            currentLoadRoomData.Z * room.length);

        room.X = currentLoadRoomData.X;
        room.Z = currentLoadRoomData.Z;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Z;
        room.transform.parent = transform;

        isLoadingRoom = false;

        if (loadedRooms.Count == 0)
        {
            CameraController.instance.currentRoom = room;
        }

        loadedRooms.Add(room);
    }

    public bool DoesRoomExist( int x, int z)
    {
        return loadedRooms.Find( item => item.X == x && item.Z == z) != null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
