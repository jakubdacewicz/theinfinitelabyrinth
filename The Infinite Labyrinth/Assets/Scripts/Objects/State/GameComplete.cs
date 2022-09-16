using System.Collections;
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
        dataPersistenceManager = GameObject.Find("DataPersistanceManager").GetComponentInChildren<DataPersistenceManager>();
        dataPersistenceManager.LoadRecords();
    }

    public override void Interact()
    {
        if (GameObject.FindWithTag("Key") != null)
        {
            audioSource.PlayOneShot(doorLocked);

            return;
        }

        audioSource.PlayOneShot(doorUnlocked);

        GameController gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        gameController.isTimerActive = false;
        gameController.isSceneChanged = true;      

        StartCoroutine(LoadEndScreen());
    }

    private IEnumerator LoadEndScreen()
    {
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

        SceneManager.LoadScene("EndScreen");
    }
}
