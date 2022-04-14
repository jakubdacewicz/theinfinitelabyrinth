using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private CharacterStats playerStats;

    public float attackDamage;

    private void Start()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats.TakeDamage(attackDamage);

            Destroy(gameObject);
        }      
    }

    private void SetAttackDamage(float damage)
    {
        attackDamage = damage;
    }
}
