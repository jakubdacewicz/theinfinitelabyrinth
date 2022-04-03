using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;

    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerControler.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        foreach (Vector2Int roomLocation in rooms)
        {         
            RoomController.instance.LoadRoom(Random.Range(1,3).ToString(), roomLocation.x, roomLocation.y);
        }
    }
}
