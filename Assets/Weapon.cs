using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[System.Serializable]
public class Weapon : MonoBehaviour,  IInventoryItem
{
    public string ID;
    public Sprite Icon;

    public List<AttachmentConfig> Attachments;

    public WeaponAnimationStates AnimationState;

    [HideInInspector]
    public int _count;
    public int Range;


    public List<WeaponBehavior> Behaviors;

 //   public WeaponBehavior RegularBehavior;
   // public WeaponBehavior IntAttackBehavior;
    
    ItemTypes IInventoryItem.GetItemType()
    {
        return ItemTypes.weapon;
    }

    public string GetID()
    {
        return ID;
    }

    public Sprite GetImage()
    {
        return Icon;
    }

    public string GetDescription()
    {
        return "";
    }

    public void AddToInventory(UnitInventory inv)
    {
        
    }

    public void RemoveFromInventory(UnitInventory inv)
    {
 
    }

    public int GetCount()
    {
        return _count;
    }


    void IInventoryItem.SetCount(int new_count)
    {
        _count = new_count;
    }
}
