using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;

    public bool isSceneChanged = false;

    private void Start()
    {
        StartCoroutine(ActivatePlayerAndCamera());
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
