using UnityEngine;

public class Block : MonoBehaviour
{
    //public
    public float playerSlowValue;

    //private
    private CharacterStats characterStats;
    private float currentMovementSpeed;
    private float nextStamineActionTime = 0;
    private float startTime = 0;

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        if (characterStats.GetCurrentStamine() < Mathf.Abs(characterStats.parringStamineCost.GetValue()))
        {
            this.enabled = false;
            GetComponent<Idle>().enabled = true;
        }
        else
        {
            //dodac animacje blokowania
            characterStats.RegenerationStamineSwitchMode(false);
            characterStats.MakePlayerInvulnerableTimeless(true);
            currentMovementSpeed = characterStats.movementSpeed.GetValue();
            characterStats.movementSpeed.SetValue(currentMovementSpeed - playerSlowValue);         
            characterStats.AdjustCurrentStamine(characterStats.parringStamineCost.GetValue());
            startTime = Time.time;

            if (characterStats.GetCurrentStamine() <= 0)
            {
                nextStamineActionTime = Time.time + characterStats.stamine0ActionDelay.GetValue();
                characterStats.RegenerationStamineSwitchMode(true);
                characterStats.MakePlayerInvulnerableTimeless(false);
                characterStats.movementSpeed.SetValue(currentMovementSpeed);
            }           
        }      
    }

    private void OnDisable()
    {
        characterStats.movementSpeed.SetValue(currentMovementSpeed);
        characterStats.RegenerationStamineSwitchMode(true);
        characterStats.MakePlayerInvulnerableTimeless(false);
    }

    private void Update()
    {
        if (Time.time >= startTime + characterStats.parringDelay.GetValue() &&nextStamineActionTime <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.enabled = false;
                GetComponent<Dash>().enabled = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                this.enabled = false;
                GetComponent<Attack>().enabled = true;
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                this.enabled = false;
                GetComponent<Block>().enabled = true;
            }
            else
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            }
        }       
    }
}
