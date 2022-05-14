using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyable : Interactable
{
    public float price;

    public bool isBought;

    public override void Interact()
    {
        CharacterStats characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();

        if (!isBought && characterStats.money.GetValue() >= price)
        {
            characterStats.money.AddValue(-price);

            GetComponent<SpawnItem>().enabled = true;

            isBought = true;
        }       
    }
}
