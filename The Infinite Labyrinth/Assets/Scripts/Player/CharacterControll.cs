using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    //public
    public float interactSphereRadius;

    //private
    private CharacterStats characterStats;
    private bool isMovementBlocked = false;
    private bool isRotationBlocked = false;

    private void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))        
            CheckInteraction();

        if (!isMovementBlocked)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position -= Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(0, 0, 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(1, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(1, 0, 0);
            }
        }

        if (!isRotationBlocked)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }
    }

    public void BlockPlayerMovement(bool action)
    {
        isMovementBlocked = action;
    }

    public void BlockPlayerRotation(bool action)
    {
        isRotationBlocked = action;
    }

    private void CheckInteraction()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactSphereRadius);

        if (colliders.Length > 0)
        {
            foreach(var collider in colliders)
            {
                if (collider.transform.GetComponent<Interactable>())
                {
                    collider.transform.GetComponent<Interactable>().Interact();
  //                  return;
                }
            }
        }
    }

    /*
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        Gizmos.color = Color.yellow;
        float value = characterStats.attackRange.GetValue();
        Gizmos.DrawWireSphere(transform.position + transform.forward * test, value);
    }
    */
}

