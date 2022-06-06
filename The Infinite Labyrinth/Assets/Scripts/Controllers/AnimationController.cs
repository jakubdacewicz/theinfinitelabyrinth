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
    public Transform unlockUITransform;

    public Queue<string> uiAnimationQueue = new Queue<string>();
    public Queue<Item> itemStatQueue = new Queue<Item>();
    public Queue<string> statsAnimationQueue = new Queue<string>();

    public CharacterStats characterStats;

    public AudioSource audioSource;

    public AudioClip showInfoBoxSound;
    public AudioClip paperSound;
    public AudioClip itemUnlockSound;

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
                
                if(uiAnimationQueue.Peek() == "newItemUnlocked")
                {
                    audioSource.pitch = 1;
                    audioSource.PlayOneShot(itemUnlockSound);
                }
                else
                {
                    StartCoroutine(PlayInfoBoxSound());
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

    public void SetUnlockUIInfo(Item item)
    {
        foreach (Transform child in unlockUITransform)
        {
            if (child.name.Equals("Image"))
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

    private IEnumerator PlayInfoBoxSound()
    {
        audioSource.pitch = 2.2f;

        audioSource.PlayOneShot(showInfoBoxSound);

        yield return new WaitForSeconds(0.8f);

        audioSource.PlayOneShot(paperSound);

        yield return new WaitForSeconds(0.2f);

        audioSource.PlayOneShot(paperSound);

        yield return new WaitForSeconds(0.2f);

        audioSource.PlayOneShot(paperSound);

        yield return new WaitForSeconds(paperSound.length + 4.1f);

        audioSource.PlayOneShot(showInfoBoxSound);
    }
}
