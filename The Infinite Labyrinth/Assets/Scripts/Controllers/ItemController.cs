using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;

    public string description;

    public GameObject itemModel;
}

public abstract class ItemController : MonoBehaviour
{
    public Item item;

    public CharacterStats characterStats;

    private void Start()
    {
        GameObject model = Instantiate(item.itemModel, transform.position, transform.rotation);
        model.transform.parent = transform;

        characterStats = GameObject.FindWithTag("Player").GetComponent<CharacterStats>();
    }

    public abstract void AddEffectToPlayer();

    private void OnTriggerEnter(Collider other)
    {
        AddEffectToPlayer();

        Debug.Log("Podniesiono przedmiot: " + gameObject.name);
        Destroy(gameObject);
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

}
