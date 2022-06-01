using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameData
{
    public List<GameObject> items;

    public int itemsBought;

    public int enemysKilled;

    public bool[] isUnlocked;

    public GameData()
    {
        items = Resources.LoadAll<GameObject>("Prefabs/Items/Default").ToList();

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
