using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;

    public string description;

    public GameObject itemModel;

    public GameObject itemPrefabUI;
}

public abstract class ItemController : MonoBehaviour
{
    public Item item;

    public CharacterStats characterStats;

    private bool isTriggered = false;

    //public GameObject itemPrefabUI;

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

            Debug.Log("Podniesiono przedmiot: " + gameObject.name);
            Destroy(gameObject);

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

            child.localPosition = child.localPosition - new Vector3(20, 0 ,0);
        }

        return new Vector3(x + 20 , 0 , 0);
    }

}
