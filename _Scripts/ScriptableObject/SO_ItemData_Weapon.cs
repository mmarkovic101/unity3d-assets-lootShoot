using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon")]
public class SO_ItemData_Weapon : SO_ItemData
{
    [field: SerializeField] public WeaponType WeaponType { get; private set; }

    public SO_ItemData_Weapon()
    {
        ItemType = ItemType.Weapon;
    }
}
