using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPoint : MonoBehaviour
{
    private Vector3 position;

    private bool isUsed = false;

    private void Start()
    {
        StartCoroutine(SetPosition());
    }

    private IEnumerator SetPosition()
    {
        yield return new WaitForSeconds(0.8f);

        position = transform.position;
    }

    public Vector3 GetPointPosition { get { return position; } }

    public bool IsPointUsed { get { return isUsed; } }

    public void UsePoint(bool action)
    {
        isUsed = action;
    }
}
