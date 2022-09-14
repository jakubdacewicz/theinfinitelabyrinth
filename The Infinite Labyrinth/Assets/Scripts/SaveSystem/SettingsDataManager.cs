using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDataManager : MonoBehaviour
{
    private SettingsData settingsData;

    private FileDataHandler fileDataHandler;

    private Canvas canvas;

    public string fileName;

    public static SettingsDataManager instance
    {
        get;
        private set;
    }

    private void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        LoadSettingsData();
        UpdateSettings();
    }

    private void Awake()
    {
        instance = this;       
    }

    public void NewSettingsData()
    {
        settingsData = new SettingsData();
    }

    public void LoadSettingsData()
    {
        settingsData = fileDataHandler.LoadSettings();

        if (settingsData == null)
        {
            NewSettingsData();
        }

        
        MainMenu menu = GameObject.Find("Panel").GetComponent<MainMenu>();
        if (menu != null)
        {
            menu.LoadData(settingsData);
        }     
    }

    public void SaveSettingsData()
    {
        MainMenu menu = GameObject.Find("Panel").GetComponent<MainMenu>();
        if (menu != null)
        {
            menu.SaveData(ref settingsData);
        }

        fileDataHandler.SaveSettings(settingsData);
    }

    public void UpdateSettings()
    {
        AudioListener.volume = settingsData.gameVolume;
        canvas.scaleFactor = settingsData.uiSize;
        Screen.SetResolution(settingsData.resolutionWidth, settingsData.resolutionHeight,
            settingsData.isFullscreen, settingsData.resolutionRefreshrate);
    }
}
