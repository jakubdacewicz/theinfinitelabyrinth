using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;

    public void GoBackToMainMenu()
    {
        //GameObject blackPanel = GameObject.FindGameObjectWithTag("BlackPanel");
        //blackPanel..enabled = true;
        //blackPanel.ShowBlackPanel();

        dataPersistenceManager.SaveGame();

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

        Invoke(nameof(LoadMenu), 1.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
