using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : ItemController
{
    public float attackSpeed;
    public float attackDamage;
    public float attackRange;

    public string objectName;

    public float attackRangePostionOffset;

    public override void AddEffectToPlayer()
    {
        Transform hand = GameObject.FindWithTag("Hand").transform;

        foreach (Transform child in hand)
        {
            if(child.name.Equals(objectName))
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        lastAndNewValueDiffrence[0] = attackDamage;
        lastAndNewValueDiffrence[1] = attackSpeed;
        lastAndNewValueDiffrence[2] = attackRange;

        characterStats.attackDamage.AddValue(attackDamage);       
        characterStats.attackSpeed.AddValue(attackSpeed);
        characterStats.attackRangePosition = attackRange;

        if(characterStats.attackRange.GetValue() != attackRange)
        {
            characterStats.attackRange.SetValue(attackRange);
            
        }        

        GameObject player = GameObject.FindWithTag("Player");


        player.transform.Find("Model").GetComponent<Animator>().SetFloat("atackSpeed", characterStats.attackSpeed.GetValue());

        this.enabled = false;
    }
}
