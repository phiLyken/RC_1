using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UniqueSelectionGroup<T>  {

    public List<T> objects;

    Action<T> unselect;
    Action<T> onselect;

    T current;

    public UniqueSelectionGroup<T> Init(List<T> _objects, Action<T> _onUnselect, Action<T> _onSelect)
    {
        objects = _objects;
        unselect = _onUnselect;
        onselect = _onSelect;
        return this;
    }

    public void Select(T ob)
    {
        if (objects.Contains(ob))
        {
            foreach ( T obj in objects)
            {

                if( ReferenceEquals(obj, ob))
                {                 
                    onselect.AttemptCall(obj);
                } else
                {
                    Unselect(obj);
                }
                current = ob;
            }
        }
    }

    void Unselect(T ob)
    {
        if (objects.Contains(ob))
        {
            unselect.AttemptCall(ob);

            if(ReferenceEquals(current, ob))
            {
               current = default(T);
            }
        }
    }

}
