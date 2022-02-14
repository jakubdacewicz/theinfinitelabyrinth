using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    //public
    public abstract void Interact();

    //private
    private Transform interactModel;
    private bool _isAnimationActivated = true;

    private void Start()
    {
        interactModel = transform.Find("InteractModel");
    }

    private void OnTriggerEnter(Collider collider)
    {
        //dodac animacje poruszania siê
        //dodac animacje przejscia
        if(collider.CompareTag("Player") && _isAnimationActivated)
            interactModel.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        //dodac animacje poruszania siê
        //dodac animacje przejscia
        if (collider.CompareTag("Player") && _isAnimationActivated)
            interactModel.gameObject.SetActive(false);
    }

    public void AnimationTurnMode(bool action)
    {
        interactModel.gameObject.SetActive(action);
        _isAnimationActivated = action;
    }
}
