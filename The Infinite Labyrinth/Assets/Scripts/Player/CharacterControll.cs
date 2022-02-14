using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    //public
    public float _interactSphereRadius;

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
            m_Rigidbody.MovePosition(transform.position + inputAxis * Time.deltaTime * characterStats.movementSpeed.GetValue());
        }              
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))        
            CheckInteraction();       
    }

    public void BlockPlayerMovement(bool action)
    {
        _isMovementBlocked = action;
    }
    
    private void CheckInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _interactSphereRadius);

        if (colliders.Length > 0)
        {
            foreach(var collider in colliders)
            {
                if (collider.transform.GetComponent<Interactable>())
                {
                    collider.transform.GetComponent<Interactable>().Interact();
                    return;
                }
            }
        }
    }
}
