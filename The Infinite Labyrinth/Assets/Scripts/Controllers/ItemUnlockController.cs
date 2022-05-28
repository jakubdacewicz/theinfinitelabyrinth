using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlockController : MonoBehaviour
{
    public GameController gameController;

    public AnimationController animationController;

    [Header("Item: Saturn")]
    public int itemsBought;
    public GameObject saturn;

    public bool[] isUnlocked;

    private void Update()
    {
        if (itemsBought == 100 && !isUnlocked[0])
        {
            StartCoroutine(nameof(UnlockItemSaturn));
        }
    }

    private IEnumerator UnlockItemSaturn()
    {
        isUnlocked[0] = true;
        gameController.items.Add(saturn);
        gameController.isItemSpawned.Add(false);

        yield return new WaitForSeconds(0.2f);

        animationController.uiAnimationQueue.Enqueue("newItemUnlocked");
    }

    public void LoadData(GameData data)
    {
        this.itemsBought = data.itemsBought;
        this.isUnlocked = data.isUnlocked;
    }

    public void SaveData(ref GameData data)
    {
        data.itemsBought = this.itemsBought;
        data.isUnlocked = this.isUnlocked;
    }
}
