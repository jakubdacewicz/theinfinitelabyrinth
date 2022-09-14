using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : Interactable
{
    private DataPersistenceManager dataPersistenceManager;
    public AudioSource audioSource;

    public AudioClip doorUnlocked;
    public AudioClip doorLocked;

    private float[] records;

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

    public void LoadData(GameData data)
    {
        records = data.timeRecords;
    }

    public void SaveData(ref GameData data)
    {
        List<float> temp = records.ToList();

        GameController gm = GameObject.Find("GameController").GetComponent<GameController>();

        temp.Add(gm.timer);
        temp.Sort();
        temp.Remove(0);

        records = temp.GetRange(0, 6).ToArray();

        data.timeRecords = records;
    }
}
