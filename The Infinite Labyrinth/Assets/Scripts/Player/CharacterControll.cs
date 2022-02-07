using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    private CharacterStats characterStats;
    private Rigidbody m_Rigidbody;

    private bool isParring = false;
    private bool isSlowed = false;
    private float nextLoseStamineTime = 0;

    private void Start()
    {
        characterStats = gameObject.GetComponent<CharacterStats>();
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        m_Rigidbody.MovePosition(transform.position + inputAxis * Time.deltaTime * characterStats.movementSpeed.GetValue());
    }

    private void Update()
    {
        //TODO rotacja. na ten moment wydaje sie byc niewykonywalna jesli nie ma zrobionej mapy.

        //right click - block
        if (Input.GetMouseButton(1))
        {
            //dodac animacje blokowania
            //dodac spowolnienie postaci podczas blokowania
            isParring = true;
            if (Time.time > nextLoseStamineTime)
            {
                nextLoseStamineTime =  Time.time + characterStats.parringLoseStamineDelay.GetValue();
                characterStats.AdjustCurrentStamine(characterStats.parringLoseStamineValue.GetValue());
            }
            //do poprawki isSlowed. nie dziala jak powinno, moze trzeba to rozwiazac inaczej.
            if (isSlowed == false)
            {
                characterStats.movementSpeed.SetValue(characterStats.movementSpeed.GetValue() / 10);
                isSlowed = true;
            }
        }
        else
        {
            if (isSlowed == true)
            {
                characterStats.movementSpeed.SetValue(characterStats.movementSpeed.GetValue() * 10);
                isSlowed = false;
            }
            isParring = false;
        }
    }

    public bool isPlayerCurrParring()
    {
        return isParring;
    }
}
