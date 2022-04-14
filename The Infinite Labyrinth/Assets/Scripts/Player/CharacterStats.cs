using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    //public
    public float maxHealth;
    public float maxStamine;

    public Stat stamineRegenerationValuePart;
    public Stat stamineRegenerationTime;
    public Stat stamine0ActionDelay;
    public Stat attackDamage;
    public Stat attackSpeed;
    public Stat attackRange;
    public Stat money;
    public Stat movementSpeed;
    public Stat rotationSpeed;
    public Stat parringDelay;
    public Stat parringStamineCost;
    public Stat dashTime;
    public Stat dashSpeed;
    public Stat dashStamineCost;
    public Stat dashInvulnerableTime;

    public Text textStamine;
    public Text textHealth;
    public Text textAttackDamage;
    public Text textMoney;

    //private
    private float currentHealth;
    private float currentStamine;

    private bool isStamineRegenerationActive = true;
    private bool isPlayerInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
        currentStamine = maxStamine;
        InvokeRepeating(nameof(RegenerateStamine), 0f , stamineRegenerationTime.GetValue());
    }

    private void Update()
    {
        //¯ycie, stamina, atak i waluta tekstowa. Pozniej trzeba dodac grafiki
        textStamine.text = currentStamine + "/" + maxStamine;
        textHealth.text = currentHealth + "/" + maxHealth;
        textAttackDamage.text = attackDamage.GetValue().ToString();
        textMoney.text = money.GetValue().ToString();

        if (transform.position.y <= -5)
        {
            TakeDamage(maxHealth);
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetCurrentStamine()
    {
        return currentStamine;
    }

    public void SetMaxHealth(float value)
    {
        if (maxHealth + value >= 0)
        {
            maxHealth += value;
        }
        else
        {
            maxHealth = 0;
        }
          
    }

    public void SetMaxStamine(float value)
    {
        if (maxStamine + value >= 0)
        {
            maxStamine += value;
        }
        else
        {
            maxStamine = 0;
        }
    }

    public void AdjustCurrentStamine(float value)
    {
        if (currentStamine + value <= maxStamine && currentStamine + value > 0)
        {
            currentStamine += value;
        }
        else if (currentStamine + value > maxStamine)
        {
            currentStamine = maxStamine;
        }
        else
        {
            currentStamine = 0;           
        }
    }

    public void Heal(float value)
    {
        value = Mathf.Abs(value);
        if (currentHealth + value > maxHealth)
        {
            value = maxHealth - currentHealth;
        }
        currentHealth += value;     
    }

    public void TakeDamage(float value)
    {
        if (isPlayerInvulnerable == false)
        {
            value = Mathf.Abs(value);
            currentHealth -= value;

            if (currentHealth <= 0)
            {
                Die();
            }
        }     
    }

    public void Die()
    {
        Debug.LogWarning("Player died!");
        //Wylaczanie skryptu po smierci. Mozna w przyslosci rozwazyc inne rozwiazanie.

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemys)
        {
            enemy.GetComponent<EnemyController>().enabled = false;
        }

        GameObject.FindWithTag("Player").SetActive(false);
    }

    private void RegenerateStamine()
    {
        if (currentStamine < maxStamine && isStamineRegenerationActive)
        {
            AdjustCurrentStamine(maxStamine/stamineRegenerationValuePart.GetValue());
        }
    }

    public void RegenerationStamineSwitchMode(bool active)
    {
        isStamineRegenerationActive = active;
    }

    public IEnumerator MakePlayerInvulnerableForTime(float time)
    {
        isPlayerInvulnerable = true;
        yield return new WaitForSeconds(time);
        isPlayerInvulnerable = false;
    }

    public void MakePlayerInvulnerableTimeless(bool action)
    {
        isPlayerInvulnerable = action;
    }
}
