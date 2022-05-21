using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        StartCoroutine("BuildEnemyArea", 1.5f);
    }

    public IEnumerator BuildEnemyArea()
    {
        yield return new WaitForSeconds(1.5f);

        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.enabled = true;
        navMeshSurface.BuildNavMesh();
    }
}