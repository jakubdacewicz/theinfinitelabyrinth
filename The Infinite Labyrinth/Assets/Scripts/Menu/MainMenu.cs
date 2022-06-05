using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject deleteButton;
    public GameObject blackPanel;

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

    private void Start()
    {
        StartCoroutine(DisableBlackScreen());
    }

    private IEnumerator DisableBlackScreen()
    {
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animator>().Play("blackPanelHide");

        yield return new WaitForSeconds(2f);

        blackPanel.SetActive(false);
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {       
        blackPanel.SetActive(true);
        blackPanel.GetComponent<Animator>().Play("blackPanelShow");

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
