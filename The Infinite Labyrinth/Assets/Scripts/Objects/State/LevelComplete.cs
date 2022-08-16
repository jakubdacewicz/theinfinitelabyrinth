using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : Interactable
{
    private DungeonGenerationData dungeonGenerationData;
    public AudioSource audioSource;

    public AudioClip doorUnlocked;
    public AudioClip doorLocked;

    private void Start()
    {
        dungeonGenerationData = RoomController.instance.GetComponent<DungeonGenerator>().dungeonGenerationData;    
    }

    public override void Interact()
    {
        //jakies warunki ukonczenia
        if (GameObject.FindWithTag("Key") != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(doorLocked);
            }            
            return;
        } 

        audioSource.PlayOneShot(doorUnlocked);

        Debug.Log("Loading next world: " + dungeonGenerationData.nextWorldName);

        GameObject.FindWithTag("GameController").GetComponent<GameController>().isSceneChanged = true;

        StartCoroutine(LoadNextLevel());

        foreach (Transform child in GameObject.FindWithTag("Player").transform)
        {
            if (child.name.Equals("Light") && child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
            }
        }

        GameObject.FindWithTag("Player").gameObject.SetActive(false);
    }

    private IEnumerator LoadNextLevel()
    {
        GameObject blackPanel = GameObject.Find("Black Panel");
        blackPanel.GetComponent<BlackPanel>().enabled = true;
        blackPanel.GetComponent<BlackPanel>().ShowBlackPanel();

        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene(dungeonGenerationData.nextWorldName + "Main");
    }
}
