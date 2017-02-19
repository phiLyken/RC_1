using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public static class UnlockableFactory
{

    public static int GetUnlockedCount <T>(this List<Unlockable<T>> unlockables) {

        int unlockedCount = 0;
        for(int i = 0; i < unlockables.Count; i++)
        {
            if (unlockables[i].IsUnlocked())
            {
                unlockedCount++;
            } 
        }

        return unlockedCount;
    }

    public static T GetHighestUnlocked<T>(this List<Unlockable<T>> unlockables)
    {
        int last_lock = -1;
        T highest = default(T);
        foreach(var unlockable in unlockables)
        {
            int current_lock = unlockable.GetLevelRequirement();

            if(current_lock > last_lock && unlockable.IsUnlocked())
            {
                highest = unlockable.Item;
                last_lock = current_lock;
            }           
        }

        return highest;
    }
    public static List<Unlockable<T>> MakeUnlockables<T>(List<T> source, Levels level, Func<T, int> GetRequirement, Func<T, string> GetID, Action<T> CallBack)
    {
        List<Unlockable<T>> unlocks = new List<Unlockable<T>>();

        foreach (var c in source)
        {
            Unlockable<T> unlockable = new Unlockable<T>(c, level, GetRequirement(c), GetID);

            if(CallBack != null)
                 unlockable.AddUnlockListener(CallBack);
            unlocks.Add(unlockable);


        }

        return unlocks;
    }


    public static List<Unlockable<T>> MakeUnlockables<T>(List<T> source, Levels level, Func<T, int> GetRequirement, Func<T, string> GetID)
    {
        return MakeUnlockables<T>(source, level, GetRequirement, GetID,null);
    }



    public static List<Unlockable<T>> MakeUnlockables<T, C>(List<C> configs, Levels level, Func<C, int> GetRequirement, Func<T, string> GetID, Func<C, T> GetItem, Action<T> CallBack)
    {
        List<Unlockable<T>> unlocks = new List<Unlockable<T>>();
        foreach (var c in configs)
        {
            Unlockable<T> unlockable = new Unlockable<T>(GetItem(c), level, GetRequirement(c), GetID);

            if(CallBack != null)
                 unlockable.AddUnlockListener(CallBack);

            unlocks.Add(unlockable);

        }

        return unlocks;
    }

    public static List<Unlockable<T>> MakeUnlockables<T, C>(List<C> configs, Levels level, Func<C, int> GetRequirement, Func<T, string> GetID, Func<C, T> GetItem)
    {
        return MakeUnlockables<T,C>(configs, level, GetRequirement, GetID, GetItem, null);
    }

}