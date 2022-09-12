using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject deleteButton;

    public BlackPanel blackPanel;

    public AudioSource source;

    public AudioClip gameStartClip;

    public Slider gameVolume;
    public Slider uiSize;
    public Toggle isFullscreen;
    public Dropdown resolutionsDropdown;

    private Resolution[] resolutions;

    private void Awake()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath, "data.json");

        if(fileDataHandler.DoesDataExists())
        {
            deleteButton.SetActive(true);
        }

        resolutions = Screen.resolutions;
        Dropdown.OptionData newData;

        resolutionsDropdown.ClearOptions();
        for(int i = 0; i < resolutions.Length; i++)
        {
            newData = new Dropdown.OptionData();
            newData.text = resolutions[i].width + " x " + resolutions[i].height;
            //newData.
            resolutionsDropdown.options.Add(newData);
        }
    }

    public void PlayGame()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {       
        blackPanel.ShowBlackPanel();

        float time = gameStartClip.length;

        source.PlayOneShot(gameStartClip);
        
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene(1);
    }

    public void DeleteSave()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath, "data.json");
        fileDataHandler.Delete();

        deleteButton.SetActive(false);
    }

    public void DeletetSettingsSave()
    {
        FileDataHandler fileDataHandler = new FileDataHandler(Application.persistentDataPath, "settings.json");
        fileDataHandler.Delete();
    }

    public void SaveData(ref SettingsData data)
    {
        data.gameVolume = gameVolume.value;
        data.uiSize = uiSize.value;
        data.isFullscreen = isFullscreen.isOn;
    }

    public void LoadData(SettingsData data)
    {
        gameVolume.value = data.gameVolume;
        uiSize.value = data.uiSize;
        isFullscreen.isOn = data.isFullscreen;
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
