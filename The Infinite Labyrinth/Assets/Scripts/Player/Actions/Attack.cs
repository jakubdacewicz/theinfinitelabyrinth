using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private void OnEnable()
    {
        this.enabled = false;
        GetComponent<Idle>().enabled = true;
    }
}
