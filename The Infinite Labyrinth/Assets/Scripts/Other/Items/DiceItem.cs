using UnityEngine;

public class DiceItem : ItemController
{
    public float money10TimesWin;
    public float money15ValueWin;
    public float money25ValueWin;
    public float healthPercentageLosse;

    public override void AddEffectToPlayer()
    {
        int value = Random.Range(1, 20);
      
        if (value <= 2) characterStats.money.SetValue(characterStats.money.GetValue() * money10TimesWin);
        else if (value <= 5) characterStats.money.AddValue(money15ValueWin);
        else if (value <= 10) characterStats.money.AddValue(money25ValueWin);
        else characterStats.TakeDamage(characterStats.GetCurrentHealth() * healthPercentageLosse);

        this.enabled = false;
    }
}
