using System.Collections.Generic;
using UnityEngine;

public class SO_ItemData : ScriptableObject
{
    public ItemType ItemType { get; protected set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public List<SO_StatItem> Stats{ get; private set; }
}
