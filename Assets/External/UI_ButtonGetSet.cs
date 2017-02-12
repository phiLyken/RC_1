using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public abstract class UI_ButtonGetSet<T> : GenericView<T> {
    /// <summary>
    /// Allows to set a default item in inspector
    /// </summary>
    public T Override;
    Action<T> CallBack;  

    void Awake()
    {
        if(Override != null)
            SetItem(Override);
    }

    public virtual void SetItem(T item, Action<T> button_Callback)
    {
        base.SetItem(item);

        CallBack = button_Callback;
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public override void SetItem(T item) 
    {
        SetItem(item, null);
    }

    public void OnClick()
    {
        CallBack.AttemptCall(m_Item);
    }



}
