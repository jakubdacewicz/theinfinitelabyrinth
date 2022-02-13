using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    //private
    private CharacterStats characterStats;

    private Rigidbody m_Rigidbody;

    private bool _isMovementBlocked = false;

    private void Start()
    {       
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        m_Rigidbody = GameObject.Find("Player").GetComponent<Rigidbody>();  
    }

    private void FixedUpdate()
    {
         //TODO rotacja. na ten moment wydaje sie byc niewykonywalna jesli nie ma zrobionej mapy.

        if (!_isMovementBlocked)
        {
            Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            m_Rigidbody.MovePosition(Time.deltaTime * characterStats.movementSpeed.GetValue() * transform.position + inputAxis);
        }             
    }

    public void BlockPlayerMovement(bool action)
    {
        _isMovementBlocked = action;
    }
}
