using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterControll : MonoBehaviour
{
    //public
    public float interactSphereRadius;
    public float attackRangePosition;

    public GameObject menu;

    public Animator playerAnimator;
    public AudioSource playerSource;

    public AudioClip attackHit;
    public AudioClip attackMiss;
    public AudioClip playerDash;
    public AudioClip playerRun;

    //private
    private CharacterStats characterStats;

    private bool isMovementBlocked = true;
    private bool isRotationBlocked = false;
    private bool hasRotatingDebuff = false;
    private bool isDoingAction = false;
    private bool isStamineEmpty = false;

    private float startTime;
    private char directionFacing;

    private void Start()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        directionFacing = 'W';
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))        
            CheckInteraction();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }

        if(isStamineEmpty)
        {
            if(characterStats.GetCurrentStamine() > 50)
            {
                isStamineEmpty = false;
            }
        }

        if (hasRotatingDebuff)
        {
            transform.Rotate(Vector3.up * 150 * Time.deltaTime, Space.Self);

            return;
        }

        if (!isRotationBlocked)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                directionFacing = 'W';
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                directionFacing = 'S';
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.eulerAngles = new Vector3(0, 270, 0);
                directionFacing = 'A';
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
                directionFacing = 'D';
            }
        }

        if (isDoingAction)
        {
            playerAnimator.SetBool("isRunning", false);
            return;
        }

        if (!isStamineEmpty)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if(!isRotationBlocked)
                {
                    isDoingAction = true;
                    isMovementBlocked = true;
                    StartCoroutine(Attack());
                }              
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                isDoingAction = true;
                StartCoroutine(Dash());
            }
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                Block();
            }
            else
            {
                characterStats.RegenerationStamineSwitchMode(true);
                characterStats.MakePlayerInvulnerableTimeless(false);

                isMovementBlocked = false;

                BlockPlayerMovement(false);
                BlockPlayerRotation(false);
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                playerAnimator.SetBool("isBlocking", false);
                BlockPlayerMovement(false);
                isRotationBlocked = false;
            }
        }       

        if (!isMovementBlocked)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(0, 0, 1);
                Run('W');
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.position -= Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(0, 0, 1);
                Run('S');
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.position -= Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(1, 0, 0);
                Run('A');
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * new Vector3(1, 0, 0);
                Run('D');
            }
            else
            {
                playerSource.Stop();
                playerAnimator.SetBool("isRunning", false);
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

    private IEnumerator Attack()
    {
        playerAnimator.Play("Attack");
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * attackRangePosition, characterStats.attackRange.GetValue());

        int enemys = 0;
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    enemys++;
                    collider.GetComponent<EnemyStats>().TakeDamage(characterStats.attackDamage.GetValue());
                }
            }
        }

        if (enemys > 0)
        {
            playerSource.volume = 0.5f;
            playerSource.PlayOneShot(attackHit);

        }
        else
        {
            playerSource.volume = 0.7f;
            playerSource.PlayOneShot(attackMiss);
        }

        yield return new WaitForSeconds(characterStats.attackSpeed.GetValue());

        isDoingAction = false;
    }

    private void Run(char button)
    {
        if (!playerSource.isPlaying)
        {
            playerSource.clip = playerRun;
            playerSource.Play();
        }

        Dictionary<char, char> direction = new Dictionary<char, char>()
        {
            {'W', 'S' },
            {'S', 'W'},
            {'A', 'D'},
            {'D', 'A'}
        };

        if(directionFacing.Equals(button))
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
            {
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.Play("Run");
            }
        } 
        else if(button.Equals(direction[directionFacing])) 
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("RunBackwards"))
            {
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.Play("RunBackwards");
            }
        }
        else
        {
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("RunToSide"))
            {
                playerAnimator.SetBool("isRunning", true);
                playerAnimator.Play("RunToSide");
            }
        }
    }

    private void Block()
    {
        if (characterStats.GetCurrentStamine() >= Mathf.Abs(characterStats.parringStamineCost.GetValue()))
        {
            BlockPlayerMovement(true);
            isRotationBlocked = true;

            if(Time.time >= startTime + characterStats.parringDelay.GetValue())
            {
                if(!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Block"))
                {
                    playerAnimator.SetBool("isBlocking", true);
                    playerAnimator.Play("Block");
                }             

                characterStats.RegenerationStamineSwitchMode(false);
                characterStats.MakePlayerInvulnerableTimeless(true);

                characterStats.AdjustCurrentStamine(characterStats.parringStamineCost.GetValue());

                startTime = Time.time;
            }                     
            CheckIsStamineEmpty();            
        }
    }

    private IEnumerator Dash()
    {
        if(characterStats.GetCurrentStamine() >= Mathf.Abs(characterStats.dashStamineCost.GetValue()))
        {
            playerSource.PlayOneShot(playerDash);

            playerAnimator.Play("Dash");
            characterStats.RegenerationStamineSwitchMode(false);
            characterStats.MakePlayerInvulnerableTimeless(true);

            BlockPlayerMovement(true);
            BlockPlayerRotation(true);

            characterStats.AdjustCurrentStamine(characterStats.dashStamineCost.GetValue());
            startTime = Time.time;

            while (Time.time < startTime + characterStats.dashTime.GetValue())
            {
                transform.position += Time.deltaTime * characterStats.movementSpeed.GetValue() * transform.forward;
                yield return null;
            }
        }
        yield return new WaitForSeconds(0.3f);

        CheckIsStamineEmpty();
        isDoingAction = false;
    }

    private void CheckIsStamineEmpty()
    {
        if (characterStats.GetCurrentStamine() <= 0)
        {
            playerAnimator.SetBool("isBlocking", false);

            characterStats.RegenerationStamineSwitchMode(true);
            characterStats.MakePlayerInvulnerableTimeless(false);
            BlockPlayerMovement(false);
            BlockPlayerRotation(false);

            isStamineEmpty = true;
        }
    }

    public void ResetPlayerPosition()
    {
        gameObject.transform.position = new Vector3(0, 1, 0);
    }

    public void MakePlayerRotateAroundItself()
    {
        hasRotatingDebuff = true;
    }

    private void OnDrawGizmos()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        Gizmos.color = Color.yellow;
        float value = characterStats.attackRange.GetValue();
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRangePosition, value);
    }
}

