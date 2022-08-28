using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth;
    public float maxStamine;

    [Header("Stats")]
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

    [Header("UI text")]
    public Text textStamine;
    public Text textHealth;
    public Text textAttackDamage;
    public Text textAttackSpeed;
    public Text textAttackRange;
    public Text textMovementSpeed;
    public Text textMoney;

    public Slider healhSlider;
    public Slider stamineSlider;

    [Header("Animators")]
    public Animator healthBarAnimator;
    public Animator panelAnimator;
    public Animator statsAnimator;
    public Animator itemMessageBox;

    public GameObject gameOverBox;

    public AudioSource source;
    public AudioSource characterSource;
    public AudioSource uiSource;

    public AudioClip takeDamageSound;
    public AudioClip blockDamageSound;
    public AudioClip gameOverSound;

    private AnimationController animationController;

    private float currentHealth;
    private float currentStamine;

    private bool isStamineRegenerationActive = true;
    private bool isPlayerInvulnerable = false;

    private void Start()
    {
        animationController = GameObject.FindGameObjectWithTag("AnimationController").GetComponent<AnimationController>();

        currentHealth = maxHealth;
        currentStamine = maxStamine;
        InvokeRepeating(nameof(RegenerateStamine), 0f , stamineRegenerationTime.GetValue());

        ActualiseStatsUI();
    }

    private void Update()
    {
        textStamine.text = currentStamine + " / " + maxStamine;
        textHealth.text = currentHealth + " / " + maxHealth;       

        healhSlider.maxValue = maxHealth;
        stamineSlider.maxValue = maxStamine;

        textMoney.text = money.GetValue().ToString();

        healhSlider.value = Mathf.Lerp(healhSlider.value, currentHealth, Time.deltaTime * 10);
        stamineSlider.value = Mathf.Lerp(stamineSlider.value, currentStamine, Time.deltaTime * 10);

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

        PlayHealthBarAnimation();
    }

    public void TakeDamage(float value)
    {
        if (isPlayerInvulnerable == false)
        {
            characterSource.PlayOneShot(takeDamageSound);

            value = Mathf.Abs(value);

            currentHealth -= value;

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().ShakeCamera();

            PlayHealthBarAnimation();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            characterSource.PlayOneShot(blockDamageSound);
        }
    }

    public void Die()
    {
        uiSource.volume = 1;
        uiSource.PlayOneShot(gameOverSound);

        itemMessageBox.Play("itemBoxMinimalize");
        panelAnimator.Play("panelChildsMinimalize");
        statsAnimator.Play("statsMinimalize");
        gameOverBox.SetActive(true);

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] traps = GameObject.FindGameObjectsWithTag("Trap");

        foreach (GameObject enemy in enemys)
        {          
            enemy.GetComponent<EnemyController>().enabled = false;
        }

        foreach (GameObject trap in traps)
        {
            trap.GetComponent<ShootingTrapController>().enabled = false;
        }

        textHealth.text = 0 + "/" + maxHealth;

        Destroy(GameObject.FindWithTag("Player"));
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

    private void PlayHealthBarAnimation()
    {
        if (currentHealth < maxHealth / 3)
        {
            healthBarAnimator.SetBool("hasLowHealth", true);
        }
        else
        {
            healthBarAnimator.SetBool("hasLowHealth", false);
        }
    }

    public void ActualiseStatsUI()
    {
        textAttackDamage.text = attackDamage.GetValue().ToString("F2");
        textAttackSpeed.text = attackSpeed.GetValue().ToString("F2");
        textAttackRange.text = attackRange.GetValue().ToString("F2");
        textMovementSpeed.text = movementSpeed.GetValue().ToString("F2");       
    }

    public void PlayMoneyCollectSound()
    {
        source.Play();
    }
}
