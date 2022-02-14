using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openable : Interactable
{
    public bool _isOpened;
    public override void Interact()
    {
        //dodac animacje
        //system przedmiotow
        if (!_isOpened)
        {
            Debug.Log(gameObject.name + " has been opened!");
            AnimationTurnMode(false);
            _isOpened = true;
            this.enabled = false;
        }       
    }
}
