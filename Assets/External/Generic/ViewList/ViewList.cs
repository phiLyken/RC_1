using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class ViewList<Item, View> where View : Component
{
    int ViewItemsCount;
    public delegate Transform GetTransformForItem(Item v);
    public GetTransformForItem GetTarget;

    public delegate View ViewBuilderDelegate(Item item, Transform target);
    public delegate void ViewListUpdate(Dictionary<Item, View> views);

    public ViewBuilderDelegate MakeView;
    public ViewListUpdate OnListCreated;
    protected Dictionary<Item, View> views;
    public Action<View> RemoveViewCallback;

    public View GetView(Item item)
    {
        View v = default(View);
        views.TryGetValue(item, out v);
        return v;
    }
    public virtual ViewList<Item, View> Init(ViewBuilderDelegate view_builder, GetTransformForItem _getTarget, List<Item> list, Action<View> RemoveCallback, int _viewitemcount)
    {
        Init(view_builder, _getTarget, RemoveCallback, _viewitemcount);
        UpdateList(list);

        return this;
    }
    public virtual ViewList<Item, View> Init(ViewBuilderDelegate view_builder, GetTransformForItem _getTarget, Action<View> _removeCallback, int _viewitemcount)
    {
        if (views != null)
        {
            foreach (var pair in views)
                GameObject.Destroy(pair.Value.gameObject);
            //  MDebug.Log("clear view list");
            views = null;
        }
        RemoveViewCallback = _removeCallback;
        GetTarget = _getTarget;
        MakeView = view_builder;
        ViewItemsCount = _viewitemcount;
        return this;
    }

    public List<View> GetViews()
    {
        return views.Select(view => view.Value).ToList();
    }
    public List<Item> GetItems()
    {
        return views.Select(view => view.Key).ToList();
    }

    protected virtual void OnUpdated(Dictionary<Item, View> OnUpdate)
    { }

    public virtual Dictionary<Item, View> UpdateList(List<Item> items)
    {
        
        List<Item> to_create = new List<Item>();
        List<View> viewstoDelete = new List<View>();

        if (views == null || views.Count == 0)
        {            
            views = new Dictionary<Item, View>();
        }

        if(!items.IsNullOrEmpty())
            items = items.GetRange(0, Mathf.Min(items.Count, ViewItemsCount));

        if (items != null && items.Count > 0)
        {
            //MDebug.Log(items.Count + " " +views.Count);
            if (views.Count > 0)
            {
                to_create = items.Where(item => !views.ContainsKey(item)).ToList();
                viewstoDelete = views.Where(pair => (pair.Key == null || !items.Contains(pair.Key)) ).Select(item => item.Value).ToList();
            }
            else
            {
                to_create = new List<Item>(items);
            }

            foreach (Item i in to_create)
            {
                View new_view = MakeView(i, GetTarget(i));

                if (new_view == null)
                {
                    Debug.LogWarning("ATTEMPTED DO ADD VIEW BUT IT'S NULL");
                }
                else
                {
                    views.Add(i, new_view);
                }
            }

            Dictionary<Item, View> new_list = new Dictionary<Item, View>();

            foreach (Item item in items)
            {
                new_list.Add(item, views[item]);
            }
            views = new Dictionary<Item, View>(new_list);
        }
        else
        {
            viewstoDelete = views.Select(pair => pair.Value).ToList();
            MDebug.Log("views to delete " + viewstoDelete.Count);
        }

        viewstoDelete.ForEach(item => {
       
            var to_delete = views.FirstOrDefault(kvp => kvp.Value == item);

            if (to_delete.Key != null)
            {
                views.Remove(to_delete.Key);
               
            }
            RemoveViewCallback(item);

        });

        OnUpdated(views);
        return views;
    }

}