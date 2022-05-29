using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public List<GameObject> items;

    public int itemsBought;

    public int enemysKilled;

    public bool[] isUnlocked;

    public GameData(List<GameObject> startItemList)
    {
        items = startItemList;

        itemsBought = 0;

        enemysKilled = 0;

        isUnlocked = new bool[]
        {
            false,
            false,
            false,
            false,
            false
        };
    }

}
