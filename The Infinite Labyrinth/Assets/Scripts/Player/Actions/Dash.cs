using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //private
    private CharacterStats characterStats;

    private CharacterControll characterControll;

    private float _startTime;
    private float _nextStamineActionTime = 0;

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        characterControll = GameObject.Find("Player").GetComponent<CharacterControll>();
        if (characterStats.GetCurrentStamine() < Mathf.Abs(characterStats.dashStamineCost.GetValue()))
        {
            this.enabled = false;
            GetComponent<Idle>().enabled = true;           
        }
        else
        {
            //dodac animacje uniku
            characterStats.RegenerationStamineSwitchMode(false);            
            characterStats.MakePlayerInvulnerableTimeless(true);
            characterControll.BlockPlayerMovement(true);
            characterControll.BlockPlayerRotation(true);

            characterStats.AdjustCurrentStamine(characterStats.dashStamineCost.GetValue());
            _startTime = Time.time;
            StartCoroutine(DashCourtine());            
        }      
    }

    private void Update()
    {
        if (Time.time >= _startTime + characterStats.dashTime.GetValue() && Time.time >= _nextStamineActionTime)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
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
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                this.enabled = false;
                GetComponent<Dash>().enabled = true;
            }
            else
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            }
        }       
    }

    private IEnumerator DashCourtine()
    {
        while (Time.time < _startTime + characterStats.dashTime.GetValue())
        {
            transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * transform.forward;
            yield return null;
        }

        if (characterStats.GetCurrentStamine() <= 0)
        {
            _nextStamineActionTime = Time.time + characterStats.stamine0ActionDelay.GetValue();
            characterStats.RegenerationStamineSwitchMode(true);
            characterStats.MakePlayerInvulnerableTimeless(false);
            characterControll.BlockPlayerMovement(false);
            characterControll.BlockPlayerRotation(false);
        }
    }
}
