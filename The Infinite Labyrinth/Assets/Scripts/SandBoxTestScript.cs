using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SandBoxTestScript : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;
    private EnemyStats enemyStats;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        navMeshAgent.speed = enemyStats.movementSpeed.GetValue();
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
    }
}
