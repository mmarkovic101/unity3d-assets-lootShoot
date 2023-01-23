using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatItem", menuName = "ScriptableObjects/StatItem")]
public class SO_StatItem : ScriptableObject
{
    [field: SerializeField] public StatType StatType { get; private set;}
    [field: SerializeField] private string _description;
    [field: SerializeField] public ValueType ValueType { get; private set;}
    [field: SerializeField] private float _value;
    [field: SerializeField] private bool _increasesWithLevel;
    [field: SerializeField] private float _levelMultiplier;
    [field: SerializeField] private bool _increasesWithQuality;
    [field: SerializeField] private float _qualityMultiplier;
    [field: SerializeField] private bool _isRoundedToWhole;

    public string GetDescription(int level, int quality)
    {
        return GetValue(level, quality) + " " + _description;
    }

    public float GetValue(int level, int quality)
    {
        float multiplier = 0f;
        if (_increasesWithLevel)
        {
            multiplier += _levelMultiplier * level;
        }
        if (_increasesWithQuality)
        {
            multiplier += _qualityMultiplier * quality;
        }

        float modifiedValue;
        modifiedValue = _value * ((1 + multiplier) - _levelMultiplier - _qualityMultiplier);

        if ((_increasesWithLevel || _increasesWithQuality) && multiplier != 0f)
        {
            if (_isRoundedToWhole) return Mathf.Round(modifiedValue);
            else return (float)Math.Round(modifiedValue, 2);
        }
        else
        {
            if (_isRoundedToWhole) return Mathf.Round(_value);
            else return (float)Math.Round(_value, 2);
        }
    }
}
