using UnityEngine;

public class SpecialItemSaturn : ItemController
{
    public override void AddEffectToPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControll>().MakePlayerRotateAroundItself();

        this.enabled = false;
    }
}
