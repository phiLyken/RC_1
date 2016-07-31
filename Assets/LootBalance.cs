using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public enum LootCategory { small, medium, large }
public enum EnemyDropCategory { none, normal, boss }
public enum ItemTypes{  weapon, armor, rest_pack, stim_pack, int_charge, dust }

[System.Serializable]
public class LootBalance : MonoBehaviour {

    public List<LootConfig> LootConfigs;
    public List<EnemyDropConfig> EnemyDropConfigs;
    public List<Item_Generic> DropAbleItems;

    public LootConfig GetLootConfig(LootCategory cat)
    {
        return LootConfigs.Where(lc => lc.Category == cat).First();
    }

    public EnemyDropConfig GetEnemyCategory(EnemyDropCategory cat)
    {
        return EnemyDropConfigs.Where(lc => lc.Category == cat).First();
    }

    public Item_Generic GetItem(ItemTypes type)
    {
        return DropAbleItems.Where(lc => lc.Type == type).FirstOrDefault();
    }

    public static LootBalance GetBalance()
    {
        return (Resources.Load("Items/LootBalance") as GameObject).GetComponent<LootBalance>() ;
    }

 
}




[System.Serializable]
public class LootContentConfig : IWeightable
{
    public Item_Generic Item;
    public float Drop_Weight;
    public MyMath.R_Range BaseAmount;

    float IWeightable.Weight
    {
        get
        {
            return Drop_Weight;
        }

        set
        {
            Drop_Weight = value;
        }
    }
}


[System.Serializable]
public class EnemyDropConfig
{
    public EnemyDropCategory Category;
    public float DropChance;
    public List<EnemyDropConfig_Weighted> Configs;
}

[System.Serializable]
public class EnemyDropConfig_Weighted : IWeightable
{
    public LootConfig config;
    public float Weight;

    float IWeightable.Weight
    {
        get
        {
            return Weight;
        }

        set
        {
            Weight = value;
        }
    }
}




