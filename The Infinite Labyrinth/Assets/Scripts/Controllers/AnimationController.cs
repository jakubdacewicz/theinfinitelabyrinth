using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    private Animator statsUIAnimator;

    public Queue<string> uiAnimationQueue = new Queue<string>();
    public Queue<Item> itemStatQueue = new Queue<Item>();

    private void Start()
    {
        statsUIAnimator = GameObject.FindWithTag("ItemPickMessageBox").GetComponent<Animator>();
    }

    private void Update()
    {
        if(!statsUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("itemPickUp ShowAnimation") 
            &&  !statsUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("itemPickUp HideAnimation"))
        {
            if (uiAnimationQueue.Count > 0)
            {
                SetItemMessageBoxInfo(itemStatQueue.Dequeue());
                statsUIAnimator.Play(uiAnimationQueue.Dequeue());
            }
        }
    }

    public void SetItemMessageBoxInfo(Item item)
    {
        Transform background = GameObject.FindWithTag("ItemPickMessageBox").transform;

        foreach (Transform child in background)
        {
            if (child.name.Equals("Title"))
            {
                child.GetComponent<Text>().text = item.name;
            }
            else if (child.name.Equals("Message"))
            {
                child.GetComponent<Text>().text = item.description;
            }
            else if (child.name.Equals("Image"))
            {
                child.GetComponent<Image>().sprite = item.texture;
            }
        }
    }
}
