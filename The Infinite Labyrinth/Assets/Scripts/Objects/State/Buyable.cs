using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyable : Interactable
{
    public float price;

    public bool isBought;

    private Collider childColldier;

    private void Start()
    {
        childColldier = gameObject.transform.GetChild(0).GetComponent<Collider>();

        childColldier.enabled = false;
    }

    public override void Interact()
    {
        CharacterStats characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();

        if (!isBought && characterStats.money.GetValue() >= price)
        {
            characterStats.money.AddValue(-price);

            childColldier.enabled = true;

            isBought = true;
        }       
    }
}
