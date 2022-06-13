using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : ItemController
{
    public float money;
    public float healthPercentage;

    public override void AddEffectToPlayer()
    {
        int value = Random.Range(1, 10);

        if (value > 5) characterStats.money.AddValue(money);
        else characterStats.TakeDamage(characterStats.GetCurrentHealth() * healthPercentage);

        this.enabled = false;
    }
}
