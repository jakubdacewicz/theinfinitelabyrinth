using UnityEngine;

public class Pickable : Interactable
{
    public override void Interact()
    {
        Destroy(gameObject);
    }
}
