using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private float _maxHealth;
    public float _currentHealth;

    public Stat stamine;

    public void Awake()
    {
        stamine.SetMaxValue(100);
        stamine.SetCurrentValue(100);
    }

    public void SetMaxHealth(float value)
    {
        _maxHealth += value;    
    }

    public void Heal(float value)
    {
        if (_currentHealth + value > _maxHealth)
        {
            value = _maxHealth - _currentHealth;
        }
        _currentHealth += value;
    }

    public void TakeDamage(float value)
    {
        _currentHealth -= value;
        Debug.Log(transform.name + "took " + value + " damage.");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Player died!");
    }
}
