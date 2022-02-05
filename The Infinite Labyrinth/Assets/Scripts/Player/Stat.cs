using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float _maxValue;
    private float _currentValue;

    public float GetMaxValue()
    {
        return _maxValue;
    }

    public void SetMaxValue(float value)
    {
        if (_maxValue + value < 0)
        {
            _maxValue = 0;
        }
        else
        {
            _maxValue += value;
        }
    }

    public float GetCurrentValue()
    {
        return _currentValue;
    }

    public void SetCurrentValue(float value)
    {
        if (_currentValue + value <= _maxValue && _currentValue + value >= 0)
        {
            _currentValue += value;
        }
        else if (_currentValue + value > _maxValue)
        {
            _currentValue = _maxValue;
        }
        else
        {
            _currentValue = 0;
        }
    }
}
