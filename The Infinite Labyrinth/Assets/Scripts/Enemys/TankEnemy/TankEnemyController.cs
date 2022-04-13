using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyController : EnemyController
{
    public override void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stopRange)
        {
            isFollowing = false;
            isAttacking = true;

            return;
        }

        transform.LookAt(player.transform.position);
        gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stats.movementSpeed.GetValue() * Time.deltaTime);
    }

    public override void Wait()
    {
        transform.LookAt(player.transform.position);

        if (currentWaitTime <= Time.time)
        {
            isAttacking = true;
            isWaiting = false;
        }
    }

    public override void Attack()
    {
        //lookat dodac
        /*
        if (stats.GetCurrentHealth() <= stats.maxHealth.GetValue() / 4)
        {           
            isFollowing = false;
            isWaiting = false;
            isRunningAway = true;
            isAttacking = false;
        }
        */

        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, stats.attackRange.GetValue());

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                player.GetComponent<CharacterStats>().TakeDamage(stats.attackDamage.GetValue());
                currentAttackWaitTime = Time.time + stats.attackCooldown.GetValue();

                break;
            }
        }

        isFollowing = true;
        isAttacking = false;
    }
}
