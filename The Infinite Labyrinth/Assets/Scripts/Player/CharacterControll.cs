using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public float _playerSlowValue;

    private CharacterStats characterStats;
    private Rigidbody m_Rigidbody;

    private bool _isParring = false;
    //    private bool _isSlowed = false;
    private float _nextLoseStamineTime = 0;
    private float _nextStamineActionTime = 0;
    private float _actionAfterDashTime = 0;
    private float _currentMovmentSpeed;

    private void Start()
    {
        characterStats = gameObject.GetComponent<CharacterStats>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        _currentMovmentSpeed = characterStats.movementSpeed.GetValue();
    }

    private void FixedUpdate()
    {
        //##PLAYER MOVMENT##
        //zoptymalizowac ify
        if (Time.time > _actionAfterDashTime)
        {
            Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_Rigidbody.MovePosition(transform.position + inputAxis * Time.deltaTime * characterStats.movementSpeed.GetValue());
        }        
    }

    private void Update()
    {
        //TODO rotacja. na ten moment wydaje sie byc niewykonywalna jesli nie ma zrobionej mapy.

        if (characterStats.GetCurrentStamine() == 0)
        {
            _nextStamineActionTime = Time.time + characterStats.stamine0ActionDelay.GetValue();
        }

        //##RIGHT CLICK - BLOCK##
        //dodac animacje blokowania
        //zoptymalizowac ify
        if (Input.GetMouseButton(1) && Time.time > _nextStamineActionTime && Time.time > _actionAfterDashTime)
        {          
            _isParring = true;
//            _isSlowed = true;
            characterStats.RegenerationStamineSwitchMode(false);
            if (Time.time > _nextLoseStamineTime)
            {
                _nextLoseStamineTime = Time.time + characterStats.parringLoseStamineDelay.GetValue();
                characterStats.AdjustCurrentStamine(characterStats.parringLoseStamineValue.GetValue());
            }

            characterStats.movementSpeed.SetValue(_currentMovmentSpeed - _playerSlowValue);                                 
        }
        //##SPACE - ROLL/DASH##
        //dodac animacje uniku
        else if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextStamineActionTime && Time.time > _actionAfterDashTime)
        {         
            if (characterStats.GetCurrentStamine() >= Mathf.Abs(characterStats.dashStamineCost.GetValue()))
            {
                _actionAfterDashTime = Time.time + characterStats.dashAnimationTime.GetValue();
                characterStats.AdjustCurrentStamine(characterStats.dashStamineCost.GetValue());
                m_Rigidbody.AddForce(transform.forward * characterStats.dashForce.GetValue());
            }
            
        }
        else
        {
            _isParring = false;
 //           _isSlowed = false;
            characterStats.movementSpeed.SetValue(_currentMovmentSpeed);
            characterStats.RegenerationStamineSwitchMode(true);
        }

        
    }

    public bool IsPlayerCurrParring()
    {
        return _isParring;
    }

    public void UpdateCurrentMovementSpeedVariable()
    {
        _currentMovmentSpeed = characterStats.movementSpeed.GetValue();
    }
}
