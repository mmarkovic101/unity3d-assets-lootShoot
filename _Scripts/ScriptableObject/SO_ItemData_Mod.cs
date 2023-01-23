using UnityEngine;

[CreateAssetMenu(fileName = "Mod", menuName = "ScriptableObjects/Mod")]
public class SO_ItemData_Mod : SO_ItemData
{
    [field: SerializeField] public ModType ModType { get; private set; }

    public SO_ItemData_Mod()
    {
        ItemType = ItemType.Mod;
    }
}
