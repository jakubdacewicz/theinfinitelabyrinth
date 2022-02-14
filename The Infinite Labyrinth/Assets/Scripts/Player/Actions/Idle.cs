using UnityEngine;

public class Idle : MonoBehaviour
{
    //private
    private CharacterStats characterStats;
    private CharacterControll characterControll;

    private void OnEnable()
    {
        characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        characterControll = GameObject.Find("Player").GetComponent<CharacterControll>();

        characterStats.RegenerationStamineSwitchMode(true);
        characterStats.MakePlayerInvulnerableTimeless(false);
        characterControll.BlockPlayerMovement(false);
    }

    private void Update()
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
        else if (Input.GetMouseButton(1))
        {
            this.enabled = false;
            GetComponent<Block>().enabled = true;
        }
    }
}
