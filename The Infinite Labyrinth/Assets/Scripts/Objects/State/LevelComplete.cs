using System.Collections;
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
        if (GameObject.FindWithTag("Key") != null)
        {
            audioSource.PlayOneShot(doorLocked); 
            
            return;
        } 

        audioSource.PlayOneShot(doorUnlocked);

        StartCoroutine(LoadNextLevel());

        GameObject player = GameObject.Find("Player");

        foreach (Transform child in player.transform)
        {
            if (child.name.Equals("Light") && child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(false);
            }
        }

        player.GetComponent<CharacterControll>().enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<BoxCollider>().enabled = false;
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
