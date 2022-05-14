using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsItem : ItemController
{
    public float health;
    public float maxHealth;
    public float movementSpeed;
    public float attackDamage;
    public float attackSpeed;
    public float attackRange;
    public float money;

    public override void AddEffectToPlayer()
    {
        characterStats.Heal(health);
        characterStats.SetMaxHealth(maxHealth);
        characterStats.movementSpeed.AddValue(movementSpeed);
        characterStats.attackDamage.AddValue(attackDamage);
        characterStats.attackRange.AddValue(attackRange);
        characterStats.money.AddValue(money);
    }
}
