using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    public Rigidbody bullet;
    public float projectileSpeed;

    public override void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stats.attackRange.GetValue())
        {
            isFollowing = false;
            isAttacking = true;

            return;
        }

        //transform.LookAt(player.transform.position);
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, stats.movementSpeed.GetValue() * Time.deltaTime);
        agent.destination = player.transform.position;
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

    public override void RunAway()
    {
        //transform.LookAt(FindClosestPoint());
        //gameObject.transform.position = Vector3.MoveTowards(transform.position, FindClosestPoint(), stats.movementSpeed.GetValue() * movementSpeedBoost * Time.deltaTime);

        agent.stoppingDistance = 0;
        agent.destination = FindClosestPoint();
        agent.speed = startMovementSpeed + movementSpeedBoost;

        if (IsEnemyAtPositionOfPoint())
        {            
            agent.stoppingDistance = stats.attackRange.GetValue();
            agent.speed -= movementSpeedBoost;

            currentWaitTime = Time.time + waitTime;

            isWaiting = true;
            isRunningAway = false;
        }
    }

    public override void Attack()
    {
        transform.LookAt(player.transform.position);

        if (stats.GetCurrentHealth() <= stats.maxHealth.GetValue() / 4)
        {           
            isFollowing = false;
            isWaiting = false;
            isRunningAway = true;
            isAttacking = false;
        }
        

        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        var shoot = Instantiate(bullet, transform.position, transform.rotation);
        shoot.SendMessage("SetAttackDamage", stats.attackDamage.GetValue());

        shoot.AddForce(transform.forward * projectileSpeed);

        currentAttackWaitTime = Time.time + stats.attackCooldown.GetValue();

        isFollowing = true;
        isAttacking = false;

        Destroy(shoot.gameObject, stats.attackCooldown.GetValue());
    }
}
