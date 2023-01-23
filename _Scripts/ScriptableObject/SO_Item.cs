using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SO_Item : ScriptableObject
{
    public int Level { get; private set; }
    public int Quality { get; private set; }
    public SO_ItemData ItemData { get; set; }
    public List<SO_Item> Mods { get; set; }

    public void SetLevel()
    {
        if (ItemData.ItemType == ItemType.Weapon)
        {
            Level = Random.Range(1, 21);
        }
        else
        {
            Level = 0;
        }
    }

    public void SetQuality()
    {
        List<int> dropRateTable = new List<int>()
            {
                80, 40, 20, 10, 5
            };
        int dropRateLeft = dropRateTable.Sum();
        int randomNumber = UnityEngine.Random.Range(0, dropRateLeft);
        for (int i = 1; i <= dropRateTable.Count; i++)
        {
            dropRateLeft -= dropRateTable[i - 1];
            if (dropRateLeft <= randomNumber)
            {
                Quality = i;
                if(ItemData.ItemType == ItemType.Weapon) SetModSlots();
                return;
            }
        }
    }

    public string GetItemDescriptionDetailed()
    {
        string itemDescription = GetItemDescriptionShort() + "\n\n";
        ItemData.Stats.ForEach(x => itemDescription += x.GetDescription(Level, Quality) + "\n");
        return itemDescription;
    }

    public string GetItemDescriptionShort()
    {
        string itemDescription = "";

        itemDescription += ItemData.Name + "\n";
        itemDescription += GetItemType() + "\n";
        itemDescription += GetQuality();
        if (Level != 0f)
        {
            itemDescription += "\nLevel: " + Level;
        }

        return itemDescription;
    }

    private string GetItemType()
    {
        switch (ItemData.ItemType)
        {
            case ItemType.Weapon:
                return "WEAPON";
            default:
                return "MOD";
        }
    }

    private string GetQuality()
    {
        switch (Quality)
        {
            case 0:
                return "";
            case 1:
                return "Common";
            case 2:
                return "Uncommon";
            case 3:
                return "Rare";
            case 4:
                return "Epic";
            default:
                return "Legendary";
        }
    }

    private void SetModSlots()
    {
        Mods = new List<SO_Item>();
        switch (Quality)
        {
            case 2:
                Mods.Add(null);
                break;
            case 3:
                Mods.Add(null);
                Mods.Add(null);
                break;
            case 4:
                Mods.Add(null);
                Mods.Add(null);
                Mods.Add(null);
                break;
            case 5:
                Mods.Add(null);
                Mods.Add(null);
                Mods.Add(null);
                Mods.Add(null);
                break;
        }
    }

}


