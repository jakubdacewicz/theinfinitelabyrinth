using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public DataPersistenceManager dataPersistenceManager;

    private void TrasitionBetweenScenes()
    {
        GameObject blackPanel = GameObject.Find("Black Panel");
        blackPanel.GetComponent<BlackPanel>().enabled = true;
        blackPanel.GetComponent<BlackPanel>().ShowBlackPanel();

        dataPersistenceManager.SaveGame();

        GameObject[] objects = new GameObject[]
        {
            GameObject.FindGameObjectWithTag("Player"),
            GameObject.FindGameObjectWithTag("GameController"),
            GameObject.FindGameObjectWithTag("AnimationController"),
            GameObject.FindGameObjectWithTag("ItemUnlockController"),
            GameObject.FindGameObjectWithTag("Manager")
        };

        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    public void GoBackToMainMenu()
    {
        TrasitionBetweenScenes();

        Invoke(nameof(LoadMenu), 1.5f);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        TrasitionBetweenScenes();

        Invoke(nameof(LoadStartGameScene), 1.5f);
    }

    private void LoadMenu()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        SceneManager.LoadScene(0);

        Destroy(canvas);
    }

    private void LoadStartGameScene()
    {
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

        SceneManager.LoadScene(1);

        Destroy(canvas);
    }
}
