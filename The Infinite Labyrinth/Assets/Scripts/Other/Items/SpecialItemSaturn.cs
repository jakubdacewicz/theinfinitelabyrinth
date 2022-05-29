using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemSaturn : ItemController
{
    public override void AddEffectToPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControll>().MakePlayerRotateAroundItself();

        this.enabled = false;
    }
}
