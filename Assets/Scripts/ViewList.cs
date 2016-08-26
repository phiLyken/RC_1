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
        MakeView = view_builder;
    }

    public Dictionary<Item, View> UpdateList(List<Item> items)
    {
        if (views == null)
        {
            views = new Dictionary<Item, View>();
        }

        List<Item> to_create = items.Where(item => !views.ContainsKey(item)).ToList();

        List<Item> items_to_delete = views.Where(item => !items.Contains(item.Key)).Select(item => item.Key).ToList();

        items_to_delete.ForEach(item => {

            GameObject.Destroy(views[item].gameObject);
            views.Remove(item);

        });


        foreach (Item i in to_create)
        {

            views.Add(i, MakeView(i));
        }


        Dictionary<Item, View> new_list = new Dictionary<Item, View>();
        foreach (Item item in items)
        {
            new_list.Add(item, views[item]);
        }
        views = new Dictionary<Item, View>(new_list);

        return views;
    }



}