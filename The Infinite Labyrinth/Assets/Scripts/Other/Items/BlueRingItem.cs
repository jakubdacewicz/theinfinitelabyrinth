public class BlueRingItem : ItemController
{
    public float healthPercentageLosse;

    public override void AddEffectToPlayer()
    {
        characterStats.SetMaxHealth(characterStats.maxHealth * -healthPercentageLosse);
        characterStats.Heal(characterStats.maxHealth);

        this.enabled = false;
    }
}
