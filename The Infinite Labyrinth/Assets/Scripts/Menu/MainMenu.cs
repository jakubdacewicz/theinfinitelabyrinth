using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button deleteButton;

    private void Awake()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath);

        if(fileDataHandler.DoesDataExists())
        {
            deleteButton.interactable = true;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath);
        fileDataHandler.Delete();

        deleteButton.interactable = false;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
