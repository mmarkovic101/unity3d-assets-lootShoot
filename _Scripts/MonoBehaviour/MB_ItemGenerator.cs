using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MB_ItemGenerator : MonoBehaviour
{
    public List<SO_Item> GeneratedItems { get; private set; }

    [field: SerializeField, Range(1, 12)] private int _generatedItemNumber;
    [field: SerializeField] private List<SO_ItemData> _itemData;

    void Start()
    {
        GeneratedItems = new List<SO_Item>();
        for (int i = 0; i < 12; i++)
        {
            GeneratedItems.Add(null);
        }
    }

    public void GenerateNewItems()
    {
        DestroyGeneratedItems();

        for (int i = 0; i < _generatedItemNumber; i++)
        {
            GeneratedItems[i] = CreateNewItem();
        }
    }

    private void DestroyGeneratedItems()
    {
        foreach (SO_Item item in GeneratedItems)
        {
            if (item != null)
            {
                DestroyMods(item);
                Destroy(item);
            }
        }
        GeneratedItems.ForEach(x => x = null);
    }

    private static void DestroyMods(SO_Item item)
    {
        if (item.Mods != null)
        {
            for (int i = 0; i < item.Mods.Count; i++)
            {
                if (item.Mods[i] != null)
                {
                    Destroy(item.Mods[i]);
                    item.Mods[i] = null;
                }
            }
        }
    }

    private SO_Item CreateNewItem()
    {
        SO_Item newItem = ScriptableObject.CreateInstance<SO_Item>();

        newItem.ItemData = _itemData[UnityEngine.Random.Range(0, _itemData.Count)];
        newItem.SetLevel();
        newItem.SetQuality();

        return newItem;
    }
}
