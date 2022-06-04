using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject deleteButton;

    public AudioSource source;

    public AudioClip gameStartClip;

    private void Awake()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath);

        if(fileDataHandler.DoesDataExists())
        {
            deleteButton.SetActive(true);
        }
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        float time = gameStartClip.length;

        source.PlayOneShot(gameStartClip);
        
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath);
        fileDataHandler.Delete();

        deleteButton.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
