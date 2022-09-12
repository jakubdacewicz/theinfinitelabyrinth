using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float gameVolume;

    public bool isFullscreen;

    public float uiSize;

    public int resolutionHeight;
    public int resolutionWidth;
    public int resolutionRefreshrate;

    public SettingsData()
    {
        gameVolume = 0.5f;
        uiSize = 1f;
        isFullscreen = true;

        resolutionHeight = Screen.currentResolution.height;
        resolutionWidth = Screen.currentResolution.width;
        resolutionRefreshrate = Screen.currentResolution.refreshRate;
    }
}
