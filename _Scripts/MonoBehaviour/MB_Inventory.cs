using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MB_Inventory : MonoBehaviour
{
    [field: SerializeField] private SO_Player _player;
    [field: SerializeField] private Text _textTotalStats;
    private MB_ItemGenerator _itemGenerator;
    private List<MB_Slot> _slots;

    void Start()
    {
        _itemGenerator = GameObject.FindGameObjectWithTag("ItemGenerator").GetComponent<MB_ItemGenerator>();
        _slots = new List<MB_Slot>();
        GetSlots();
        _textTotalStats.text = _player.ShowTotalStatsOnStart();
    }

    public void CloseInventory()//CloseButton
    {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GenerateNewItems()//GenerateButton
    {
        _itemGenerator.GenerateNewItems();
    }

    public void UpdateSlots()//GenerateButton
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].Item = null;
        }

        _slots[0].Item = _player.EquippedWeapon;
        
        if (_player.EquippedWeapon != null)
        {
            if(_player.EquippedWeapon.Quality > 1) _slots[1].Item = _player.EquippedWeapon.Mods[0];
            if(_player.EquippedWeapon.Quality > 2) _slots[2].Item = _player.EquippedWeapon.Mods[1];
            if(_player.EquippedWeapon.Quality > 3) _slots[3].Item = _player.EquippedWeapon.Mods[2];
            if(_player.EquippedWeapon.Quality > 4) _slots[4].Item = _player.EquippedWeapon.Mods[3];
        }
        else
        {
            _slots[1].Item = null;
            _slots[2].Item = null;
            _slots[3].Item = null;
            _slots[4].Item = null;
        }

        for (int i = 5; i < _itemGenerator.GeneratedItems.Count + 5; i++)
        {
            _slots[i].Item = _itemGenerator.GeneratedItems[i - 5];
        }

        HideUnusedModSLots();
        _player.CalculateTotalStats(_player.EquippedWeapon);
        _textTotalStats.text = _player.PrintTotalStats();
    }

    private void HideUnusedModSLots()
    {
        if (_player.EquippedWeapon == null)
        {
            _slots[1].transform.gameObject.SetActive(false);
            _slots[2].transform.gameObject.SetActive(false);
            _slots[3].transform.gameObject.SetActive(false);
            _slots[4].transform.gameObject.SetActive(false);
        }
        else
        {
            _slots[1].transform.gameObject.SetActive(false);
            _slots[2].transform.gameObject.SetActive(false);
            _slots[3].transform.gameObject.SetActive(false);
            _slots[4].transform.gameObject.SetActive(false);

            if (_player.EquippedWeapon.Quality > 1) _slots[1].transform.gameObject.SetActive(true);
            if (_player.EquippedWeapon.Quality > 2) _slots[2].transform.gameObject.SetActive(true);
            if (_player.EquippedWeapon.Quality > 3) _slots[3].transform.gameObject.SetActive(true);
            if (_player.EquippedWeapon.Quality > 4) _slots[4].transform.gameObject.SetActive(true);
        }
    }

    private void GetSlots()
    {
        GameObject slotWeapon = GameObject.FindGameObjectWithTag("SlotWeapon");
        _slots.Add(slotWeapon.GetComponent<MB_Slot>());

        List<GameObject> slotsMod = GameObject.FindGameObjectsWithTag("SlotMod").ToList();
        slotsMod = slotsMod.OrderBy(x => x.transform.name).ToList();
        slotsMod.ForEach(x => _slots.Add(x.GetComponent<MB_Slot>()));

        List<GameObject> slotsGeneratedItem = GameObject.FindGameObjectsWithTag("SlotGeneratedItem").ToList();
        slotsGeneratedItem = slotsGeneratedItem.OrderBy(x => x.transform.name).ToList();
        slotsGeneratedItem.ForEach(x => _slots.Add(x.GetComponent<MB_Slot>()));
    }
}
