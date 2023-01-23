using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/Player")]
public class SO_Player : ScriptableObject
{

    [field: SerializeField] public List<SO_StatTotal> TotalStats { get; private set; }
    public SO_Item EquippedWeapon { get; set; }
    
    public string ShowTotalStatsOnStart()
    {
        TotalStats.ForEach(x => x.ResetTotalStat());
        return PrintTotalStats();
    }

    public void CalculateTotalStats(SO_Item equippedWeapon)
    {
        if (equippedWeapon != null)
        {
            TotalStats.ForEach(x => x.ResetTotalStat());

            foreach (SO_StatItem statItem in equippedWeapon.ItemData.Stats)
            {
                TotalStats.Find(x => x.StatType == statItem.StatType).AddItemStat(statItem, equippedWeapon.Level, equippedWeapon.Quality);
            }
            foreach (SO_Item mod in equippedWeapon.Mods)
            {
                if (mod != null)
                {
                    foreach (SO_StatItem statItem in mod.ItemData.Stats)
                    {
                        TotalStats.Find(x => x.StatType == statItem.StatType).AddItemStat(statItem, mod.Level, mod.Quality);
                    }
                }
            }
        }
    }

    public string PrintTotalStats()
    {
        string textTotalStats = "";
        foreach (SO_StatTotal totalStat in TotalStats)
        {
            textTotalStats += totalStat.GetDescription();
        }
        return textTotalStats;
    }
}
