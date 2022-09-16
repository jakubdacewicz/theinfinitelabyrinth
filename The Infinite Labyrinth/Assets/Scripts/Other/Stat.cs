using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private float baseValue;

    public float GetValue()
    {
        return baseValue;
    }

    public void AddValue(float value)
    {
        if (baseValue + value >= 0)
        {
            baseValue += value;
        }
        else
        {
            baseValue = 0;
        }
    }

    public void SetValue(float value)
    {
        baseValue = value;
    }
}
