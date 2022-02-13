using UnityEngine;

public class Block : MonoBehaviour
{
    //public
    public float _playerSlowValue;

    //private
    private CharacterStats characterStats;
    private float _currentMovementSpeed;
    private float _nextStamineLoseTime = 0;
    private float _nextStamineActionTime = 0;   

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        if (characterStats.GetCurrentStamine() < Mathf.Abs(characterStats.parringLoseStamineValue.GetValue()))
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
        }      
    }

    private void OnDisable()
    {
        characterStats.movementSpeed.SetValue(_currentMovementSpeed);
    }

    private void Update()
    {
        if (_nextStamineActionTime <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.enabled = false;
                GetComponent<Dash>().enabled = true;
            }
            else if (Input.GetMouseButton(0))
            {
                this.enabled = false;
                GetComponent<Attack>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                this.enabled = false;
                GetComponent<Interact>().enabled = true;
            }
            else if (Input.GetMouseButton(1))
            {
                if (Time.time > _nextStamineLoseTime)
                {
                    _nextStamineLoseTime = Time.time + characterStats.parringLoseStamineDelay.GetValue();
                    characterStats.AdjustCurrentStamine(characterStats.parringLoseStamineValue.GetValue());
                    if (characterStats.GetCurrentStamine() <= 0)
                    {
                        _nextStamineActionTime = Time.time + characterStats.stamine0ActionDelay.GetValue();
                        characterStats.RegenerationStamineSwitchMode(true);
                        characterStats.MakePlayerInvulnerableTimeless(false);
                        characterStats.movementSpeed.SetValue(_currentMovementSpeed);
                    }
                }
            }
            else
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            }
        }       
    }
}
