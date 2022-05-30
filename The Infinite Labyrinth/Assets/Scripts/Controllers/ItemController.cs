using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]

public class Item
{
    public string name;

    public string description;

    public Sprite texture;

    public GameObject itemModel;

    public GameObject itemPrefabUI;
}

public abstract class ItemController : MonoBehaviour
{
    public Item item;

    public CharacterStats characterStats;

    private bool isTriggered = false;

    public float[] lastAndNewValueDiffrence = new float[4];

    private void Start()
    {
        GameObject model = Instantiate(item.itemModel, transform.position, transform.rotation);
        model.transform.parent = transform;

        StartCoroutine(nameof(LoadCharacterStats));
    }

    public abstract void AddEffectToPlayer();

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            AddEffectToPlayer();

            AnimationController animationController = GameObject.FindGameObjectWithTag("AnimationController").GetComponent<AnimationController>();

            animationController.itemStatQueue.Enqueue(item);
            animationController.uiAnimationQueue.Enqueue("itemPickUp ShowAnimation");
            animationController.QueueStatsAnimation(lastAndNewValueDiffrence);

            GameObject createImage = Instantiate(item.itemPrefabUI, CalculateUIItemPosition(), item.itemPrefabUI.transform.rotation);
            createImage.transform.SetParent(GameObject.FindGameObjectWithTag("PickUpUi").transform, false);

            GetComponent<BoxCollider>().enabled = false;

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }          

            StartCoroutine(nameof(DestroyItemAfterTime));

            isTriggered = true;
        }      
    }

    private Vector3 CalculateUIItemPosition()
    {
        Transform pickUpUI = GameObject.FindGameObjectWithTag("PickUpUi").transform;

        float x = -20;

        foreach (Transform child in pickUpUI)
        {
            if(child.position.x > x)
            {
                x = child.localPosition.x;
            }

            child.localPosition -=  new Vector3(20, 0 ,0);
        }

        return new Vector3(x + 20 , 0 , 0);
    }

    private IEnumerator DestroyItemAfterTime()
    {
        yield return new WaitForSeconds(4f);

        Destroy(gameObject);
    }

    private IEnumerator LoadCharacterStats()
    {
        yield return new WaitForSeconds(1);

        characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
    }
}
