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

    private bool isRunAwayActionDone = false;

    public override void Follow()
    {
        if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= stats.attackRange.GetValue())
        {
            isFollowing = false;
            isAttacking = true;
            animator.SetBool("isRunning", false);

            return;
        }

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.SetBool("isRunning", true);
            animator.Play("Run");
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
        agent.destination = currentDestination.GetPointPosition;
        agent.speed = startMovementSpeed + movementSpeedBoost;

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            animator.SetBool("isRunning", true);
            animator.Play("Run");
        }

        if (IsEnemyAtPositionOfPoint())
        {            
            agent.speed -= movementSpeedBoost;

            currentWaitTime = Time.time + waitTime;

            isRunAwayActionDone = true;
            isRunningAway = false;
            isWaiting = true;

            animator.SetBool("isRunning", false);
        }
    }

    public override void Attack()
    {
        transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

        if (stats.GetCurrentHealth() <= stats.maxHealth.GetValue() / 4 && !isRunAwayActionDone)
        {           
            isFollowing = false;
            isWaiting = false;           
            isAttacking = false;
            currentDestination = FindPoint();
            isRunningAway = true;

            return;
        }
        

        if (currentAttackWaitTime > Time.time)
        {
            return;
        }

        animator.Play("Attack");

        var shoot = Instantiate(bullet, shooter.position, transform.rotation);      
        shoot.SendMessage("SetAttackDamage", stats.attackDamage.GetValue());

        shoot.AddForce(transform.forward * projectileSpeed);

        currentAttackWaitTime = Time.time + stats.attackCooldown.GetValue();
      
        isAttacking = false;
        isFollowing = true;

        Destroy(shoot.gameObject, stats.attackCooldown.GetValue() + projectileDisapiranceTime);
    }
}
