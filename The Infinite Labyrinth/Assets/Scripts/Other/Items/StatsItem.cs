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
        characterStats.attackSpeed.AddValue(attackSpeed);
        characterStats.attackRange.AddValue(attackRange);
        characterStats.money.AddValue(money);

        GameObject player = GameObject.FindWithTag("Player");
        Animator animator = player.transform.Find("Model").GetComponent<Animator>();

        animator.SetFloat("atackSpeed", characterStats.attackSpeed.GetValue());
        animator.SetFloat("runSpeed", characterStats.movementSpeed.GetValue());

        lastAndNewValueDiffrence[0] = attackDamage;
        lastAndNewValueDiffrence[1] = attackSpeed;
        lastAndNewValueDiffrence[2] = attackRange;
        lastAndNewValueDiffrence[3] = movementSpeed;

        this.enabled = false;
    }
}
