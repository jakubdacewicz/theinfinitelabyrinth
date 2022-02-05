using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat : MonoBehaviour
{
    [SerializeField] private float baseValue;

    public float GetValue()
    {
        return baseValue;
    }
}