using System.Collections;
using Unity.AI.Navigation;
using UnityEngine.AI;
using UnityEngine;

public class NavMeshController : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        StartCoroutine("BuildEnemyArea");
    }

    public IEnumerator BuildEnemyArea()
    {
        yield return new WaitForSeconds(2.5f);

        navMeshSurface = GetComponent<NavMeshSurface>();
        navMeshSurface.enabled = true;
        navMeshSurface.BuildNavMesh();
    }
}
