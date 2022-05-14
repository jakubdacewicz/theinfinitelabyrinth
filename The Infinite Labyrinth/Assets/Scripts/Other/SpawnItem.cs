using System.Collections;
using System.Collections.Generic;
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
                GameObject item = Instantiate(gameController.items[randomIndex], transform.position, transform.rotation);
                item.transform.parent = transform;
                gameController.isItemSpawned[randomIndex] = true;

                break;
            }
        } while (gameController.isItemSpawned[randomIndex]);
    }
}
