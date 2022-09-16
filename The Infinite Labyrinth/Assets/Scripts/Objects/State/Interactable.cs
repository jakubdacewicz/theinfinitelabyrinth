using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
    public abstract void Interact();
}
