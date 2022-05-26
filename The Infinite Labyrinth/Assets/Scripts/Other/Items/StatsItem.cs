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

    private float[] lastAndNewValueDiffrence = new float[4];

    public override void AddEffectToPlayer()
    {
        characterStats.Heal(health);
        characterStats.SetMaxHealth(maxHealth);
        characterStats.movementSpeed.AddValue(movementSpeed);
        characterStats.attackDamage.AddValue(attackDamage);
        characterStats.attackSpeed.AddValue(attackSpeed);
        characterStats.attackRange.AddValue(attackRange);
        characterStats.money.AddValue(money);

        AnimationController animationController = GameObject.FindGameObjectWithTag("AnimationController").GetComponent<AnimationController>();

        animationController.itemStatQueue.Enqueue(item);
        animationController.uiAnimationQueue.Enqueue("itemPickUp ShowAnimation");

        lastAndNewValueDiffrence[0] = attackDamage;
        lastAndNewValueDiffrence[1] = attackSpeed;
        lastAndNewValueDiffrence[2] = attackRange;
        lastAndNewValueDiffrence[3] = movementSpeed;

        animationController.QueueStatsAnimation(lastAndNewValueDiffrence);
    }
}
