using UnityEngine;
using UnityEngine.EventSystems;

public class MB_DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IDropHandler
{
    [field: SerializeField] private SO_Player _player;
    private MB_ItemGenerator _itemGenerator;
    private static MB_Slot _dragSlot;
    private static MB_Slot _dropSlot;
    private MB_Inventory _inventory;
    private MB_PlayerControls _playerControls;

    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<MB_Inventory>();
        _itemGenerator = GameObject.FindGameObjectWithTag("ItemGenerator").GetComponent<MB_ItemGenerator>();
        _playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<MB_PlayerControls>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        eventData.pointerEnter.TryGetComponent<MB_Slot>(out _dragSlot);
    }

    public void OnDrag(PointerEventData eventData) { }

    public void OnDrop(PointerEventData eventData)
    {
        if (_dragSlot.Item != null && eventData.pointerEnter.TryGetComponent<MB_Slot>(out _dropSlot))
        {
            SwitchItem();
            _inventory.UpdateSlots();
            _playerControls.SwitchWeapon();
        }
    }

    private void SwitchItem()
    {
        if (_dragSlot.ItemType == ItemType.GeneratedItem && _dropSlot.ItemType == ItemType.Weapon
            && _dragSlot.Item.ItemData.ItemType == ItemType.Weapon)
        {
            int indexDrag = _itemGenerator.GeneratedItems.IndexOf(_dragSlot.Item);
            _player.EquippedWeapon = _dragSlot.Item;
            _itemGenerator.GeneratedItems[indexDrag] = _dropSlot.Item;
        }
        if (_dragSlot.ItemType == ItemType.Weapon && _dropSlot.ItemType == ItemType.GeneratedItem
            && _dropSlot.Item == null)
        {
            int indexDrop = _itemGenerator.GeneratedItems.IndexOf(_dropSlot.Item);
            _itemGenerator.GeneratedItems[indexDrop] = _dragSlot.Item;
            _player.EquippedWeapon = _dropSlot.Item;
        }
        if (_dragSlot.ItemType == ItemType.GeneratedItem && _dropSlot.ItemType == ItemType.Mod
            && _dragSlot.Item.ItemData.ItemType == ItemType.Mod)
        {
            int indexDrag = _itemGenerator.GeneratedItems.IndexOf(_dragSlot.Item);
            int indexDrop = _player.EquippedWeapon.Mods.IndexOf(_dropSlot.Item);
            _player.EquippedWeapon.Mods[indexDrop] = _dragSlot.Item;
            _itemGenerator.GeneratedItems[indexDrag] = _dropSlot.Item;
        }
        if (_dragSlot.ItemType == ItemType.Mod && _dropSlot.ItemType == ItemType.GeneratedItem
            && _dropSlot.Item == null)
        {
            int indexDrag = _player.EquippedWeapon.Mods.IndexOf(_dragSlot.Item);
            int indexDrop = _itemGenerator.GeneratedItems.IndexOf(_dropSlot.Item);
            _itemGenerator.GeneratedItems[indexDrop] = _dragSlot.Item;
            _player.EquippedWeapon.Mods[indexDrag] = _dropSlot.Item;
        }
    }
}
