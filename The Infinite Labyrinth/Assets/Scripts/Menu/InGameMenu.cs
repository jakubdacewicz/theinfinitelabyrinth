using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;

    public void GoBackToMainMenu()
    {
        GameObject[] objects = new GameObject[]
        {
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("Canvas"),
            GameObject.FindGameObjectWithTag("GameController"),
            GameObject.FindGameObjectWithTag("AnimationController"),
            GameObject.FindGameObjectWithTag("ItemUnlockController"),
            GameObject.FindGameObjectWithTag("Manager")
        };

        foreach (GameObject obj in objects)
        {
            SceneManager.MoveGameObjectToScene(obj, SceneManager.GetActiveScene());
        }

        dataPersistenceManager.SaveGame();

        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
