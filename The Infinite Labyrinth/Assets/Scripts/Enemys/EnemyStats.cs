using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{ 
    public Stat maxHealth;
    public Stat movementSpeed;
    public Stat attackRange;
    public Stat attackDamage;
    public Stat attackCooldown;
    public Stat goldDroped;

    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth.GetValue();
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void TakeDamage(float value)
    {
        currentHealth -= value;

        if (currentHealth <= 0)
        {
            Debug.LogWarning(gameObject.name +  " killed.");
            Die();           
        }
    }

    private void Die()
    {
        CharacterStats characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        
        characterStats.money.AddValue(goldDroped.GetValue());
        characterStats.PlayMoneyCollectSound();

        GameObject.FindGameObjectWithTag("ItemUnlockController").GetComponent<ItemUnlockController>().enemysKilled++;

        GetComponentInChildren<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyController>().enabled = false;
        GetComponentInParent<Room>().DecreaseEnemyAmmount();    
    }
}
