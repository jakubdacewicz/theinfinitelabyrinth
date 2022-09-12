using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public bool isActive;

    private void Start()
    {
        Cursor.visible = isActive;

        if (isActive)
        {
            Cursor.lockState = CursorLockMode.None;

            return;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
}
