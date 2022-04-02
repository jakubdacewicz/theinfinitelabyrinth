using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int x;
    public int z;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    public List<Room> loadedRooms = new List<Room>();

    string currentWorldName = "Desert";

    private RoomInfo currentLoadRoomData;

    private Room currentRoom;

    private Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    private bool isLoadingRoom = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadRoom("Start", 0, 0);
        LoadRoom("1", 1, 0);
        LoadRoom("2", -1, 0);
        LoadRoom("1", 0, 1);
        LoadRoom("2", 0, -1);
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
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void LoadRoom(string name, int x, int z)
    {
        if (DoesRoomExist(x, z))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.x = x;
        newRoomData.z = z;

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
            currentLoadRoomData.x * room.width,
            0,
            currentLoadRoomData.z * room.length);

        room.x = currentLoadRoomData.x;
        room.z = currentLoadRoomData.z;
        room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.x + ", " + room.z;
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
        return loadedRooms.Find( item => item.x == x && item.z == z) != null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
