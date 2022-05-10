using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.AI.Navigation;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public bool isSceneChanged = false;

    public Text gameTime;

    private NavMeshSurface navMeshSurface;

    private float timer = 0f;

    private void Start()
    {
        navMeshSurface = gameObject.GetComponent<NavMeshSurface>();
        Invoke("BuildEnemyArea", 1.5f);
        StartCoroutine(ActivatePlayerAndCamera());
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        timer += Time.deltaTime;

        gameTime.text = String.Format("{0:00}", (int)timer / 60) + ":" + String.Format("{0:00}", (int)timer % 60);
    }

    private void BuildEnemyArea()
    {
        navMeshSurface.enabled = true;
        navMeshSurface.BuildNavMesh();
    }

    private IEnumerator ActivatePlayerAndCamera()
    {
        yield return new WaitForSeconds(0.8f);


        CharacterControll characterControll = player.GetComponent<CharacterControll>();
        characterControll.ResetPlayerPosition();
        characterControll.BlockPlayerMovement(false);

        player.SetActive(true);
//        Debug.Log("Player activated.");

        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().LockCamera(false);
//       Debug.Log("Camera unlocked.");
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
