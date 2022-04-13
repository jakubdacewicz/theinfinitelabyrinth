using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{ 
    public Stat maxHealth;
    public Stat movementSpeed;
    public Stat attackRange;
    public Stat attackDamage;
    public Stat attackCooldown;

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
            Debug.LogWarning("Enemy killed.");
            Die();           
        }
    }

    private void Die()
    {
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        gameObject.GetComponent<EnemyController>().enabled = false;
    }
}
