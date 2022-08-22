using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : Interactable
{
    private DataPersistenceManager dataPersistenceManager;
    public AudioSource audioSource;

    public AudioClip doorUnlocked;
    public AudioClip doorLocked;

    private void Start()
    {
        dataPersistenceManager = GameObject.FindWithTag("Manager").GetComponentInChildren<DataPersistenceManager>();
    }

    public override void Interact()
    {
        if (GameObject.FindWithTag("Key") != null)
        {
            audioSource.PlayOneShot(doorLocked);

            return;
        }

        audioSource.PlayOneShot(doorUnlocked);

        GameObject.FindWithTag("GameController").GetComponent<GameController>().isSceneChanged = true;

        StartCoroutine(LoadEndScreen());
    }

    private IEnumerator LoadEndScreen()
    {
        Debug.Log("Saving game.");
        dataPersistenceManager.SaveGame();

        GameObject blackPanel = GameObject.Find("Black Panel");
        blackPanel.GetComponent<BlackPanel>().enabled = true;
        blackPanel.GetComponent<BlackPanel>().ShowBlackPanel();

        yield return new WaitForSeconds(4f);

        GameObject[] objects = new GameObject[]
       {
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("Manager"),
            GameObject.FindGameObjectWithTag("AnimationController"),
            GameObject.FindGameObjectWithTag("Canvas"),
       };

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }

        yield return new WaitForSeconds(0.5f);

        Debug.Log("Loading End Screen.");

        SceneManager.LoadScene("EndScreen");
    }
}
