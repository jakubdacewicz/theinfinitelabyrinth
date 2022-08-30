using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemSwitch : ItemController
{
    public float movementSpeed;

    public override void AddEffectToPlayer()
    {
        RenderSettings.fog = true;

        GameObject.FindWithTag("SceneLight").GetComponent<Animator>().enabled = true;

        GameObject.FindWithTag("MainCamera").GetComponent<Animator>().enabled = true;

        foreach (Transform child in GameObject.FindWithTag("Player").transform)
        {
            if(child.name.Equals("Light"))
            {
                child.gameObject.SetActive(true);
            }
        }

        characterStats.movementSpeed.AddValue(movementSpeed);

        GameObject player = GameObject.FindWithTag("Player");

        player.transform.Find("Model").GetComponent<Animator>().SetFloat("runSpeed", characterStats.movementSpeed.GetValue());

        lastAndNewValueDiffrence[3] = movementSpeed;

        this.enabled = false;
    }
}
