using UnityEngine;

public class Block : MonoBehaviour
{
    //public
    public float _playerSlowValue;

    //private
    private CharacterStats characterStats;
    private float _currentMovementSpeed;
    private float _nextStamineActionTime = 0;
    private float _startTime = 0;

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
            _currentMovementSpeed = characterStats.movementSpeed.GetValue();
            characterStats.movementSpeed.SetValue(_currentMovementSpeed - _playerSlowValue);         
            characterStats.AdjustCurrentStamine(characterStats.parringStamineCost.GetValue());
            _startTime = Time.time;

            if (characterStats.GetCurrentStamine() <= 0)
            {
                _nextStamineActionTime = Time.time + characterStats.stamine0ActionDelay.GetValue();
                characterStats.RegenerationStamineSwitchMode(true);
                characterStats.MakePlayerInvulnerableTimeless(false);
                characterStats.movementSpeed.SetValue(_currentMovementSpeed);
            }           
        }      
    }

    private void OnDisable()
    {
        characterStats.movementSpeed.SetValue(_currentMovementSpeed);
        characterStats.RegenerationStamineSwitchMode(true);
        characterStats.MakePlayerInvulnerableTimeless(false);
    }

    private void Update()
    {
        if (Time.time >= _startTime + characterStats.parringDelay.GetValue() &&_nextStamineActionTime <= Time.time)
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
