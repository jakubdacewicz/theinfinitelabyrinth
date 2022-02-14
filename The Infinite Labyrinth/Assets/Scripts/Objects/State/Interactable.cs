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

    public abstract void Interact();
    private Transform interactModel;

    private void Start()
    {
        interactModel = transform.Find("InteractModel");
    }

    private void OnTriggerEnter(Collider collider)
    {
        //dodac animacje poruszania si�
        //dodac animacje przejscia
        if(collider.CompareTag("Player"))
            interactModel.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider collider)
    {
        //dodac animacje poruszania si�
        //dodac animacje przejscia
        if (collider.CompareTag("Player"))
            interactModel.gameObject.SetActive(false);
    }
}
