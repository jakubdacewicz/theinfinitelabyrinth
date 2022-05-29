using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemLighter : ItemController
{
    public float attackDamage;

    public override void AddEffectToPlayer()
    {
        //podpalenie fx i dot
        
        lastAndNewValueDiffrence[0] = attackDamage;

        this.enabled = false;
    }
}
