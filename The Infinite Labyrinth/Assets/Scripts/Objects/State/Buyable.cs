using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buyable : Interactable
{
    public float price;

    public bool isBought;

    private Collider childColldier;

    public AudioClip notEnoughMoneySound;
    private AudioSource audioSource;

    private void Start()
    {
        childColldier = gameObject.transform.GetChild(0).GetComponent<Collider>();
        audioSource = gameObject.GetComponent<AudioSource>();

        childColldier.enabled = false;
    }

    public override void Interact()
    {
        CharacterStats characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();

        if (!isBought && characterStats.money.GetValue() >= price)
        {
            characterStats.money.AddValue(-price);

            childColldier.enabled = true;

            ItemUnlockController itemUnlockController = GameObject.FindWithTag("ItemUnlockController").GetComponent<ItemUnlockController>();
            itemUnlockController.itemsBought++;
            itemUnlockController.currentItemsBought++;

            isBought = true;
        } 
        else if(!isBought && characterStats.money.GetValue() < price) 
        {
            audioSource.PlayOneShot(notEnoughMoneySound);
        }     
    }
}
