using System;
using UnityEngine;
using UnityEngine.UI;

public class MB_Slot : MonoBehaviour
{
    [field: SerializeField] public ItemType ItemType { get; private set; }

    [field: SerializeField] private Text _title;
    [field: SerializeField] private Image _icon;
    [field: SerializeField] private Color _colorQ1;
    [field: SerializeField] private Color _colorQ2;
    [field: SerializeField] private Color _colorQ3;
    [field: SerializeField] private Color _colorQ4;
    [field: SerializeField] private Color _colorQ5;

    private SO_Item _item;
    public SO_Item Item
    {
        get
        {
            return _item;
        }
        set
        {
            _item = value;
            UpdateSlot();
        }
    }

    private void UpdateSlot()
    {
        if (_item == null)
        {
            _icon.sprite = null;
            _title.text = "";
            gameObject.GetComponent<Image>().color = Color.white;
        }
        else
        {
            _icon.sprite = _item.ItemData.Icon;
            _title.text = _item.ItemData.Name;
            gameObject.GetComponent<Image>().color = GetColor();
        }
    }

    private Color GetColor()
    {
        switch (_item.Quality)
        {
            case 1:
                return _colorQ1;
            case 2:
                return _colorQ2;
            case 3:
                return _colorQ3;
            case 4:
                return _colorQ4;
            default:
                return _colorQ5;
        }
    }
}
