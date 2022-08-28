using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemBrush : ItemController
{
    public float speedAttack;

    public override void AddEffectToPlayer()
    {
        GameObject.FindWithTag("Player").GetComponent<Animator>().enabled = true;

        lastAndNewValueDiffrence[1] = -speedAttack;

        this.enabled = false;
    }
}
