using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public bool isSceneChanged = false;

    public Text gameTime;

    public List<GameObject> items;
    public List<bool> isItemSpawned;

    private float timer = 0f;

    private void Start()
    {
        StartCoroutine(ActivatePlayerAndCamera());

        for (int i  = 0; i < items.Count; i++)
        {
            isItemSpawned.Add(false);
        }
    }

    private void Update()
    {
        if(isSceneChanged)
        {
            StartCoroutine(ActivatePlayerAndCamera());
            isSceneChanged = false;
        }

        if (player.GetComponent<CharacterStats>().GetCurrentHealth() <= 0)
        {
            return;
        }
        /*
        timer += Time.deltaTime;

        gameTime.text = String.Format("{0:00}", (int)timer / 60) + ":" + String.Format("{0:00}", (int)timer % 60);
        */
    }

    private IEnumerator ActivatePlayerAndCamera()
    {
        yield return new WaitForSeconds(0.8f);

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
