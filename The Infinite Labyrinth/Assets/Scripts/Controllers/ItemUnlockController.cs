using System.Collections;
using UnityEngine;

public class ItemUnlockController : MonoBehaviour
{
    public GameController gameController;

    public AnimationController animationController;

    public int itemsBought;
    public int enemysKilled;

    public int currentItemsBought;
    public int currentEnemysKilled;

    [Header("Item: Saturn")]   
    public GameObject saturn;

    [Header("Item: Brush")]
    public GameObject brush;

    [Header("Item: Switch")]
    public GameObject switchPrefab;

    [Header("Item: Medal")]
    public GameObject medal;

    public bool[] isUnlocked;

    private void Update()
    {
        if (itemsBought == 100 && !isUnlocked[0])
        {
            StartCoroutine(UnlockItem(0, saturn));
        }

        if (itemsBought == 200 && !isUnlocked[1])
        {
            StartCoroutine(UnlockItem(1, brush));
        }

        if (enemysKilled == 200 && !isUnlocked[2])
        {
            StartCoroutine(UnlockItem(2, switchPrefab));
        }

        if (enemysKilled == 400 && !isUnlocked[3])
        {
            StartCoroutine(UnlockItem(3, medal));
        }
    }

    private IEnumerator UnlockItem(int unlockedIndex, GameObject item)
    {
        isUnlocked[unlockedIndex] = true;
        gameController.items.Add(item);
        gameController.isItemSpawned.Add(false);

        yield return new WaitForSeconds(0.2f);

        animationController.SetUnlockUIInfo(item.GetComponent<ItemController>().item);
        animationController.uiAnimationQueue.Enqueue("newItemUnlocked");
    }

    public void LoadData(GameData data)
    {
        this.itemsBought = data.itemsBought;
        this.isUnlocked = data.isUnlocked;
        this.enemysKilled = data.enemysKilled;
    }

    public void SaveData(ref GameData data)
    {
        data.itemsBought = data.itemsBought + currentItemsBought;
        data.isUnlocked = this.isUnlocked;
        data.enemysKilled = data.enemysKilled + currentEnemysKilled;
    }
}
