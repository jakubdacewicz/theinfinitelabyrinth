using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackPanel : MonoBehaviour
{
    public float secoundsFromStart;

    private void Awake()
    {
        StartCoroutine(HideBlackPanel());
    }

    private IEnumerator HideBlackPanel()
    {
        yield return new WaitForSeconds(secoundsFromStart);

        GetComponent<Image>().enabled = true;
        GetComponent<Animator>().Play("blackPanelHide");

        yield return new WaitForSeconds(1.3f);

        GetComponent<Image>().enabled = false;      
    }

    public void ShowBlackPanel()
    {
        GetComponent<Image>().enabled = true;
        GetComponent<Animator>().Play("blackPanelShow");
    }
}
