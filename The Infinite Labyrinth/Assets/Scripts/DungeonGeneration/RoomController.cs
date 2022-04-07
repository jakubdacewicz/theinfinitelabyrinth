using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    string currentWorldName = "Forrest";

    RoomInfo currentLoadRoomData;

    private Room currentRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool updatedRooms = false;

    private void Awake()
    {
        instance = this;
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
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (spawnedBossRoom && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    StartCoroutine(room.RemoveUnconectedDoors());                   
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    public void SetWorldName(string name)
    {
        currentWorldName = name;
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;

        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            int shopRoomSpawn = (loadedRooms.Count - 1) / 6;
            if (shopRoomSpawn == 0)
            {
                shopRoomSpawn++;
            }
            LoadSpecialRoom(shopRoomSpawn, "Shop");
            LoadSpecialRoom((loadedRooms.Count - 1) / 3, "End");
            LoadSpecialRoom(loadedRooms.Count - 1, "Key");
        }
    }

    private void LoadSpecialRoom(int replacedRoomNumber, string name)
    {
        Room bossRoom = loadedRooms[replacedRoomNumber];
        Room tempRoom = gameObject.AddComponent<Room>();
        tempRoom.SetXandZ(bossRoom.X, bossRoom.Z);
        Destroy(bossRoom.gameObject);

        var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Z == tempRoom.Z);
        loadedRooms.Remove(roomToRemove);

        LoadRoom(name, tempRoom.X, tempRoom.Z);
        Destroy(tempRoom);
    }

    public void LoadRoom(string name, int x, int z)
    {
        if (DoesRoomExist(x, z))
        {
            return;
        }
        
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Z = z;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName;

        if(string.Equals(info.name, "Shop"))       
            roomName = info.name;
        else
            roomName = currentWorldName + info.name;
        

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Z))
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
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
        
    }

    public bool DoesRoomExist( int x, int z)
    {
        return loadedRooms.Find( item => item.X == x && item.Z == z) != null;
    }

    public Room FindRoom(int x, int z)
    {
        return loadedRooms.Find(item => item.X == x && item.Z == z);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currentRoom = room;
        currentRoom = room;
    }
}
