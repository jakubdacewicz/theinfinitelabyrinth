using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    [Header("Ranged Enemy")]
    public Rigidbody bullet;

    public float projectileSpeed;
    public float projectileDisapiranceTime;

    public Transform shooter;

    public override void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stats.attackRange.GetValue())
        {
            isFollowing = false;
            isAttacking = true;

            return;
        }

        agent.destination = player.transform.position;
    }

    public override void Wait()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (currentWaitTime <= Time.time)
        {           
            isWaiting = false;
            isAttacking = true;
        }
    }

    public override void RunAway()
    {
        agent.stoppingDistance = 0;
        agent.destination = FindClosestPoint();
        agent.speed = startMovementSpeed + movementSpeedBoost;

        if (IsEnemyAtPositionOfPoint())
        {            
            agent.stoppingDistance = stats.attackRange.GetValue();
            agent.speed -= movementSpeedBoost;

            currentWaitTime = Time.time + waitTime;
          
            isRunningAway = false;
            isWaiting = true;
        }
    }

    public override void Attack()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (stats.GetCurrentHealth() <= stats.maxHealth.GetValue() / 4)
        {           
            isFollowing = false;
            isWaiting = false;           
            isAttacking = false;
            isRunningAway = true;
        }
        

        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        var shoot = Instantiate(bullet, shooter.position, transform.rotation);      
        shoot.SendMessage("SetAttackDamage", stats.attackDamage.GetValue());

        shoot.AddForce(transform.forward * projectileSpeed);

        currentAttackWaitTime = Time.time + stats.attackCooldown.GetValue();
      
        isAttacking = false;
        isFollowing = true;

        Destroy(shoot.gameObject, stats.attackCooldown.GetValue() + projectileDisapiranceTime);
    }
}
