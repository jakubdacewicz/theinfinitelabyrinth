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
        SceneManager.sceneLoaded += OnSceneLoaded;

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
        //Debug.Log("Player activated.");

        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().enabled = true;
        //Debug.Log("Camera unlocked.");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Contains("Main") && isSceneChanged == true)
        {
           StartCoroutine(ActivatePlayerAndCamera());
           isSceneChanged = false;
        }
    }

}
