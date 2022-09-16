using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    private GameController gameController;

    private void OnEnable()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        int randomIndex;

        do
        {
            randomIndex = Random.Range(0, gameController.isItemSpawned.Count);
            if (gameController.isItemSpawned[randomIndex] == false)
            {
                GameObject item = Instantiate(gameController.items[randomIndex], transform.position + new Vector3(0, 0.05f, 0), Quaternion.Euler(80, 0 ,0));
                item.transform.parent = transform;
                gameController.isItemSpawned[randomIndex] = true;

                break;
            }
        } while (gameController.isItemSpawned[randomIndex]);
    }
}
