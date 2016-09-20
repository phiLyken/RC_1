using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ViewList<Item, View> where View : MonoBehaviour
{
    public delegate View ViewBuilderDelegate(Item item);
    public delegate void ViewListUpdate(Dictionary<Item, View> views);

    public ViewBuilderDelegate MakeView;
    public ViewListUpdate OnListCreated;
    Dictionary<Item, View> views;

    public void Init(ViewBuilderDelegate view_builder)
    {
        if(views != null)
        {
            foreach (var pair in views)
                GameObject.Destroy(pair.Value.gameObject);
          //  Debug.Log("clear view list");
            views = null;
        }
        MakeView = view_builder;
    }

    void RemoveViews()
    {

    }
    public Dictionary<Item, View> UpdateList(List<Item> items)
    {    
        
        List<Item> to_create = new List<Item>();
        List<View> viewstoDelete = new List<View>();
      
        if (views == null || views.Count == 0)
        {
            views = new Dictionary<Item, View>();
            
        }
        if (items != null && items.Count > 0)
        {
          //  Debug.Log(items.Count + " " +views.Count);
            if (views.Count > 0)
            {
                   to_create = items.Where(item => !views.ContainsKey(item)).ToList();
                   viewstoDelete = views.Where(pair => pair.Key == null || !items.Contains(pair.Key)  ).Select(item => item.Value).ToList();
            } else
            {
                to_create = new List<Item>(items);
            } 

            foreach (Item i in to_create)
            {
               // Debug.Log(i.ToString());
                views.Add(i, MakeView(i));
            }


            Dictionary<Item, View> new_list = new Dictionary<Item, View>();

            foreach (Item item in items)
            {
                new_list.Add(item, views[item]);
            }
            views = new Dictionary<Item, View>(new_list);
        } else
        {
            viewstoDelete = views.Select(pair => pair.Value).ToList();
        }


        viewstoDelete.ForEach(item => {

            GameObject.Destroy(item.gameObject);

            var to_delete = views.FirstOrDefault(kvp => kvp.Value == item);

            if(to_delete.Key != null)
             views.Remove(to_delete.Key);

        });

        return views;
    }
    
}