using System.Collections;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private GameObject player;
    private GameController gameController;

    private void Start()
    {
        StartCoroutine(ActivatePlayerAndCamera());
    }

    private IEnumerator ActivatePlayerAndCamera()
    {
        yield return new WaitForSeconds(2f);

        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        CharacterControll characterControll = player.GetComponent<CharacterControll>();
        characterControll.ResetPlayerPosition();
        characterControll.BlockPlayerMovement(false);

        player.GetComponent<CharacterControll>().enabled = true;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<BoxCollider>().enabled = true;

        Camera.main.GetComponent<CameraController>().enabled = true;

        gameController.isTimerActive = true;
    }
}
