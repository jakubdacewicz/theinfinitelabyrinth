using UnityEngine;

public class FigureItem : ItemController
{
    public float health;
    public float attackDamage;
    public float movementSpeed;

    public override void AddEffectToPlayer()
    {
        int value = Random.Range(1, 4);

        switch (value)
        {
            case 1:
                lastAndNewValueDiffrence[0] = 0;
                lastAndNewValueDiffrence[3] = 0;

                characterStats.SetMaxHealth(health);
                break;

            case 2:
                lastAndNewValueDiffrence[3] = 0;
                lastAndNewValueDiffrence[0] = attackDamage;

                characterStats.attackDamage.AddValue(attackDamage);
                break;

            case 3:
                lastAndNewValueDiffrence[0] = 0;
                lastAndNewValueDiffrence[3] = movementSpeed;

                characterStats.movementSpeed.AddValue(movementSpeed);

                GameObject player = GameObject.FindWithTag("Player");

                player.transform.Find("Model").GetComponent<Animator>().SetFloat("runSpeed", characterStats.movementSpeed.GetValue());
                break;
        }

        this.enabled = false;
    }
}
