using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator itemBoxUIAnimator;

    public Animator attackDamageAnimator;
    public Animator attackSpeedAnimator;
    public Animator attackRangeAnimator;
    public Animator movementSpeedAnimator;

    public Transform background;

    public Queue<string> uiAnimationQueue = new Queue<string>();
    public Queue<Item> itemStatQueue = new Queue<Item>();
    public Queue<string> statsAnimationQueue = new Queue<string>();

    public CharacterStats characterStats;

    private void Update()
    {
        if(itemBoxUIAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            if (uiAnimationQueue.Count > 0)
            {
                if(itemStatQueue.Count > 0)
                {
                    SetItemMessageBoxInfo(itemStatQueue.Dequeue());
                }               
                itemBoxUIAnimator.Play(uiAnimationQueue.Dequeue());
            }
        }

        if(attackDamageAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty") 
            && attackSpeedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty")
            && attackRangeAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty")
            && movementSpeedAnimator.GetCurrentAnimatorStateInfo(0).IsName("Empty"))
        {
            if (statsAnimationQueue.Count > 0)
            {
                attackDamageAnimator.Play(statsAnimationQueue.Dequeue());
                attackSpeedAnimator.Play(statsAnimationQueue.Dequeue());
                attackRangeAnimator.Play(statsAnimationQueue.Dequeue());
                movementSpeedAnimator.Play(statsAnimationQueue.Dequeue());

                characterStats.ActualiseStatsUI();
            }           
        }
    }

    public void SetItemMessageBoxInfo(Item item)
    {
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

    public void QueueStatsAnimation(float[] diffrence)
    {
        foreach (float d in diffrence)
        {
            if (d > 0)
            {
                statsAnimationQueue.Enqueue("statsUp");
            }
            else if (d < 0)
            {
                statsAnimationQueue.Enqueue("statsDown");
            }
            else
            {
                statsAnimationQueue.Enqueue("Empty");
            }
        }
    }
}
