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

        GameObject.FindWithTag("GameController").GetComponent<GameController>().isSceneChanged = true;

        SceneManager.LoadScene(dungeonGenerationData.nextWorldName + "Main");

        GameObject.FindWithTag("Player").gameObject.SetActive(false);
    }
}
