using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _baseValue;

    public float GetValue()
    {
        return _baseValue;
    }

    public void SetValue(float value)
    {
        if (_baseValue + value >= 0)
        {
            _baseValue += value;
        }
        else
        {
            _baseValue = 0;
        }
    }
}
