using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : Interactable
{
    private bool _IsOpen;
    public override void Interact()
    {
        if (!_IsOpen)
        {
            //dodac animacje
            //system przedmiotow
            Debug.Log(gameObject.name + " has been opened!");
            _IsOpen = true;
        }
    }
}
