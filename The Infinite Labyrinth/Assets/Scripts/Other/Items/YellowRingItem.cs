using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowRingItem : ItemController
{
    public float damageChangeMultipler;

    public override void AddEffectToPlayer()
    {
        float newAttackDamage;
        int value = Random.Range(1, 3);

        switch (value)
        {
            case 1:
                newAttackDamage = characterStats.attackDamage.GetValue() * damageChangeMultipler;

                lastAndNewValueDiffrence[0] = newAttackDamage - characterStats.attackDamage.GetValue();
                characterStats.attackDamage.SetValue(newAttackDamage);
                break;

            case 2:
                newAttackDamage = characterStats.attackDamage.GetValue() / damageChangeMultipler;

                lastAndNewValueDiffrence[0] = newAttackDamage - characterStats.attackDamage.GetValue();
                characterStats.attackDamage.SetValue(newAttackDamage);
                break;
        }

        this.enabled = false;
    }
}
