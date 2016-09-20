using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class AlphaStack
{
    List<AlphaStackItem> Items;
    public FloatEventHandler OnUpdate;

    public EventHandler StackUpdated;

    public AlphaStack()
    {
        Items = new List<AlphaStackItem>();
    }
    public void AddItem(AlphaStackItem item)
    {
        if (Items.Contains(item))
        {
            return;
        }

        Items.Add(item);

        if (OnUpdate != null)
            OnUpdate(GetHighestAlpha());

    }

    public void RemoveItem(AlphaStackItem item)
    {
        if(Items.Remove(item)){ 

            if (OnUpdate != null)
                OnUpdate(GetHighestAlpha());
        }
    }

    public float GetHighestAlpha()
    {
        if (Items.IsNullOrEmpty())
            return 0;

        return Items.Select(item => item.Alpha).Max();
    }

    [System.Serializable]
    public class AlphaStackItem
    {
        string id;
        public float Alpha;
        public AlphaStackItem(float alpha, string _id)
        {
            id = _id;
            Alpha = alpha;
        }
    }
}