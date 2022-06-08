using UnityEngine;

public class Attack : MonoBehaviour
{
    //public
    public float attackRangePosition;

    public AudioClip attackHit;
    public AudioClip attackMiss;

    public AudioSource attackSource;

    //private
    private CharacterStats characterStats;
    private float nextActionTime = 0;

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        DoAttack();
    }

    private void Update()
    {
        if (nextActionTime <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.enabled = false;
                GetComponent<Dash>().enabled = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                this.enabled = false;
                GetComponent<Block>().enabled = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                this.enabled = false;
                GetComponent<Attack>().enabled = true;
            }
            else
            {
                this.enabled = false;
                GetComponent<Idle>().enabled = true;
            }
        }
    }

    private void DoAttack()
    {   
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
                    //Debug.Log(collider.name + " took damage.");
                }
            }
        }

        if(enemys > 0)
        {
            attackSource.volume = 0.5f;
            attackSource.PlayOneShot(attackHit);
            
        }
        else
        {
            attackSource.volume = 0.7f;
            attackSource.PlayOneShot(attackMiss);
        }
        nextActionTime = Time.time + characterStats.attackSpeed.GetValue();
    }

    private void OnDrawGizmos()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        Gizmos.color = Color.yellow;
        float value = characterStats.attackRange.GetValue();
        Gizmos.DrawWireSphere(transform.position + transform.forward * attackRangePosition, value);
    }
}
