using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public bool isSceneChanged = false;
    public bool isTimerActive;

    public Text timerText;

    public List<GameObject> items;
    public List<bool> isItemSpawned;

    private GameObject player;

    public float timer = 0f;

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
    }

    public void LoadData(GameData data)
    {
       this.items = data.items;
    }

    public void SaveData(ref GameData data)
    {
        data.items = this.items;
    }

}
