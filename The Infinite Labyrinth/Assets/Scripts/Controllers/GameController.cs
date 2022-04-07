using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        StartCoroutine(ActivatePlayerAndCamera());
    }

    private IEnumerator ActivatePlayerAndCamera()
    {
        yield return new WaitForSeconds(1f);

        player.SetActive(true);
        Debug.Log("Player activated.");

        GameObject.FindWithTag("MainCamera").GetComponent<CameraController>().LockCamera(false);
        Debug.Log("Camera unlocked.");
    }

    
}
