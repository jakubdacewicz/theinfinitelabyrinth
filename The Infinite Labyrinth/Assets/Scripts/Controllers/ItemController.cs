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

    private void Start()
    {
        GameObject model = Instantiate(item.itemModel, transform.position, transform.rotation);
        model.transform.parent = transform;

        characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
    }

    public abstract void AddEffectToPlayer();

    private void OnTriggerEnter(Collider other)
    {
        if(!isTriggered)
        {
            AddEffectToPlayer();

            GameObject createImage = Instantiate(item.itemPrefabUI, CalculateUIItemPosition(), item.itemPrefabUI.transform.rotation);
            createImage.transform.SetParent(GameObject.FindGameObjectWithTag("PickUpUi").transform, false);

            //Debug.Log("Podniesiono przedmiot: " + gameObject.name);

            GetComponent<BoxCollider>().enabled = false;
            GetComponent<StatsItem>().enabled = false;

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            Transform background = GameObject.FindWithTag("ItemPickMessageBox").transform;

            foreach (Transform child in background)
            {
                if(child.name.Equals("Title"))
                {
                    child.GetComponent<Text>().text = item.name;
                } else if(child.name.Equals("Message"))
                {
                    child.GetComponent<Text>().text = item.description;
                } else if(child.name.Equals("Image"))
                {
                    child.GetComponent<Image>().sprite = item.texture;
                }
            }

            StartCoroutine(nameof(ShowItemPickUpMessageAndDestroyItem));          

            isTriggered = true;
        }      
    }

    public IEnumerator PlayAnimation(Animator animator, float value)
    {
        if (value > 0)
        {
            animator.SetFloat("valueChange", 1);
        }
        else if (value < 0)
        {
            animator.SetFloat("valueChange", -1);
        }

        yield return new WaitForSeconds(2);

        animator.SetFloat("valueChange", 0);
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

    private IEnumerator ShowItemPickUpMessageAndDestroyItem()
    {
        Animator itemMessage = GameObject.FindWithTag("ItemPickMessageBox").GetComponent<Animator>();

        itemMessage.SetBool("playAnimation", true);

        yield return new WaitForSeconds(0.5f);

        itemMessage.SetBool("playAnimation", false);

        Destroy(gameObject);
    }
}
