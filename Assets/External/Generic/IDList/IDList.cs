using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class IDList<T>  {


    Dictionary<string, T> table;

    public IDList  (List<IDConfig<T>> list){

        table = list.MakeDictionairy(conf => { return conf.ID; }, conf =>{return conf.item;});

    }

    public T GetItem(string ID)
    {
        if (table.ContainsKey(ID))
        {
            return table[ID];
        }

        return default(T);
    } 

    public bool HasID(string ID)
    {
        return !table.IsNullOrEmpty() && table.ContainsKey(ID);
    }

    
}


[System.Serializable]
public class IDConfig<T>
{
 
        public string ID;
        public T item;

   
}