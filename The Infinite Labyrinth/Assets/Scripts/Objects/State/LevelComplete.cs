using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : Interactable
{
    private DungeonGenerationData dungeonGenerationData;

    private void Start()
    {
        dungeonGenerationData = RoomController.instance.GetComponent<DungeonGenerator>().dungeonGenerationData;    
    }

    public override void Interact()
    {
        //jakies warunki ukonczenia
        Debug.Log("Loading next world: " + dungeonGenerationData.nextWorldName);

        SceneManager.LoadScene(dungeonGenerationData.nextWorldName + "Main");
    }
}
