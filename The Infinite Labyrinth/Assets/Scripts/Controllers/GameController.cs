using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class GameController : MonoBehaviour
{
    public bool isSceneChanged = false;
    public bool isTimerActive;

    public Text timerText;

    public List<GameObject> items;
    public List<bool> isItemSpawned;

    private GameObject player;

    public float timer = 0f;

    private float[] records;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null) return;

        StartCoroutine(ActivatePlayerAndCamera());

        for (int i  = 0; i < items.Count; i++)
        {
            isItemSpawned.Add(false);
        }
    }

    private void Update()
    {
        if (player == null) return;

        if (isSceneChanged)
        {
            StartCoroutine(ActivatePlayerAndCamera());
            isSceneChanged = false;         
        }       

        if (player.GetComponent<CharacterStats>().GetCurrentHealth() <= 0) return;

        if (!isTimerActive) return;

        timer += Time.deltaTime;

        timerText.text = String.Format("{0:00}", (int)timer / 3600) + ":" + String.Format("{0:00}", (int)(timer % 3600) / 60) + ":" + String.Format("{0:00}", (int)timer % 60);

    }

    private IEnumerator ActivatePlayerAndCamera()
    {
        yield return new WaitForSeconds(1.5f);

        CharacterControll characterControll = player.GetComponent<CharacterControll>();
        characterControll.ResetPlayerPosition();
        characterControll.BlockPlayerMovement(false);

        player.SetActive(true);     

        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().enabled = true;

        isTimerActive = true;
    }

    public void LoadData(GameData data)
    {
       items = data.items;
       records = data.timeRecords;

    }

    public void SaveData(ref GameData data)
    {
        data.items = items;

        records = records.Where(val => val != 0).ToArray();
        List<float> temp = records.ToList();

        temp.Add(timer);
        temp.Sort();

        if(temp.Count > 6)
        {
            records = temp.GetRange(0, 6).ToArray();
        }
        else
        {
            records = temp.ToArray();
        }

        data.timeRecords = records;
    }

    public float[] GetRecordsArray()
    {
        return records;
    }
    
    public bool IsCurrentTimeRecord()
    {
        return timer == records[0];
    }

}
