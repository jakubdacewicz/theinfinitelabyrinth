using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData
{
    public float gameVolume;

    public bool isFullscreen;

    public float uiSize;

    public SettingsData()
    {
        gameVolume = 0.5f;
        uiSize = 1f;
        isFullscreen = true;
    }
}
