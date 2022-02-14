using UnityEngine;

public class Attack : MonoBehaviour
{
    //public
    public Vector3 attackRangePosition;

    //private
    private CharacterStats characterStats;
    private float _nextActionTime = 0;

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        DoAttack();
    }

    private void Update()
    {
        if (_nextActionTime <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.enabled = false;
                GetComponent<Dash>().enabled = true;
            }
            else if (Input.GetMouseButton(1))
            {
                this.enabled = false;
                GetComponent<Block>().enabled = true;
            }
            else if (Input.GetMouseButton(0))
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
        //atak dealy i speed. trzeba to rozpatrzyc czy potrzebne sa obie.
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + attackRangePosition, characterStats.attackRange.GetValue());

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    //tu wstawic ze enemy traci hp.
                    Debug.Log(collider.name + " took damage.");
                }
            }
        }
        _nextActionTime = Time.time + characterStats.attackDelay.GetValue();
    }
}
