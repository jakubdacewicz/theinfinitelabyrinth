using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemBrush : ItemController
{
    public float speedAttack;

    public override void AddEffectToPlayer()
    {
        characterStats.attackSpeed.AddValue(speedAttack);

        GameObject player = GameObject.FindWithTag("Player");

        player.GetComponent<Animator>().enabled = true;
        player.transform.Find("Model").GetComponent<Animator>().SetFloat("atackSpeed", characterStats.attackSpeed.GetValue());

        lastAndNewValueDiffrence[1] = speedAttack;

        this.enabled = false;
    }
}
