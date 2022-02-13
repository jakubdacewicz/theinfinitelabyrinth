using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    //public
    public float _maxHealth;
    public float _maxStamine;

    public Stat stamineRegenerationValuePart;
    public Stat stamineRegenerationTime;
    public Stat stamine0ActionDelay;
    public Stat attackDamage;
    public Stat money;
    public Stat movementSpeed;
    public Stat rotationSpeed;
    public Stat parringLoseStamineDelay;
    public Stat parringLoseStamineValue;
    public Stat dashTime;
    public Stat dashSpeed;
    public Stat dashStamineCost;
    public Stat dashInvulnerableTime;

    //TODO zrobic aby obiekty byly ladowane automatycznie i private
    public Text textStamine;
    public Text textHealth;
    public Text textAttackDamage;
    public Text textMoney;

    //private
    private float _currentHealth;
    private float _currentStamine;

    private bool _isStamineRegenerationActive = true;
    private bool _isPlayerInvulnerable = false;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentStamine = _maxStamine;
        InvokeRepeating(nameof(RegenerateStamine), 0f , stamineRegenerationTime.GetValue());
    }

    private void Update()
    {
        //¯ycie, stamina, atak i waluta tekstowa. Pozniej trzeba dodac grafiki
        textStamine.text = _currentStamine + "/" + _maxStamine;
        textHealth.text = _currentHealth + "/" + _maxHealth;
        textAttackDamage.text = attackDamage.GetValue().ToString();
        textMoney.text = money.GetValue().ToString();
    }

    public float GetCurrentHealth()
    {
        return _currentHealth;
    }

    public float GetCurrentStamine()
    {
        return _currentStamine;
    }

    public void SetMaxHealth(float value)
    {
        if (_maxHealth + value >= 0)
        {
            _maxHealth += value;
        }
        else
        {
            _maxHealth = 0;
        }
          
    }

    public void SetMaxStamine(float value)
    {
        if (_maxStamine + value >= 0)
        {
            _maxStamine += value;
        }
        else
        {
            _maxStamine = 0;
        }
    }

    public void AdjustCurrentStamine(float value)
    {
        if (_currentStamine + value <= _maxStamine && _currentStamine + value > 0)
        {
            _currentStamine += value;
        }
        else if (_currentStamine + value > _maxStamine)
        {
            _currentStamine = _maxStamine;
        }
        else
        {
            _currentStamine = 0;           
        }
    }

    public void Heal(float value)
    {
        value = Mathf.Abs(value);
        if (_currentHealth + value > _maxHealth)
        {
            value = _maxHealth - _currentHealth;
        }
        _currentHealth += value;     
    }

    public void TakeDamage(float value)
    {
        if (_isPlayerInvulnerable == false)
        {
            value = Mathf.Abs(value);
            _currentHealth -= value;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }     
    }

    public void Die()
    {
        Debug.LogWarning("Player died!");
        //Wylaczanie skryptu po smierci. Mozna w przyslosci rozwazyc inne rozwiazanie.
        this.enabled = false;
    }

    private void RegenerateStamine()
    {
        if (_currentStamine < _maxStamine && _isStamineRegenerationActive)
        {
            AdjustCurrentStamine(_maxStamine/stamineRegenerationValuePart.GetValue());
        }
    }

    public void RegenerationStamineSwitchMode(bool active)
    {
        _isStamineRegenerationActive = active;
    }

    public IEnumerator MakePlayerInvulnerableForTime(float time)
    {
        _isPlayerInvulnerable = true;
        yield return new WaitForSeconds(time);
        _isPlayerInvulnerable = false;
    }

    public void MakePlayerInvulnerableTimeless(bool action)
    {
        _isPlayerInvulnerable = action;
    }
}
