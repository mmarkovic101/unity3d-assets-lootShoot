using System;
using UnityEngine;

[CreateAssetMenu(fileName = "StatTotal", menuName = "ScriptableObjects/StatTotal")]
public class SO_StatTotal : ScriptableObject
{
    [field: SerializeField] public StatType StatType { get; private set;}
    [field: SerializeField] private string _description;
    [field: SerializeField] private float _defaultValue;
    [field: SerializeField] private bool _isRoundedToWhole;
    public float Value { get; private set; }
    private float _baseValue;
    private float _modifier;

    public string GetDescription()
    {
        return Value + _description + "\n";
    }

    public void ResetTotalStat()
    {
        _baseValue = _defaultValue;
        _modifier = 0f;
        CalculateValue();
    }

    public void AddItemStat(SO_StatItem itemStat, int itemLevel, int itemQuality)
    {
        if (itemStat.ValueType == ValueType.BaseValue)
        {
            _baseValue += itemStat.GetValue(itemLevel, itemQuality);
        }
        else
        {
            _modifier += itemStat.GetValue(itemLevel, itemQuality);
        }
        CalculateValue();
    }

    private void CalculateValue()
    {
        if (_modifier != 0f)
        {
            Value = _baseValue * (1f + _modifier / 100);
        }
        else
        {
            Value = _baseValue;
        }

        if (_isRoundedToWhole)
        {
            Value = Mathf.Round(Value);
        }
        else
        {
            Value = (float)Math.Round(Value, 2);
        }
    }
}
