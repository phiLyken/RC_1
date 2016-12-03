﻿using UnityEngine;
using System.Collections;
using System;

public class UI_PopupController : MonoBehaviour {

    public bool WaitForOpend;
    public bool WaitForClosed;
    public bool Active;

    public Action OnClose;
    public Action OnOpen;

    public Action OnCloseDone;
    public Action OnOpenDone;

    public void Opened()
    {

        if (OnOpenDone != null)
        { 
            OnOpenDone();
        }
        Active = true;
    }

    public void Closed()
    {
        
        if (OnCloseDone != null)
            OnCloseDone();

        Active = false;


    }

    public void Close()
    {
        if (Active)
        { 
            if (OnClose != null)
                OnClose();

            if (!WaitForClosed)
                Closed();
        }
    }

    public void Open()
    {
        if (!Active)
        { 
            if (OnOpen != null)
                OnOpen();

            if (!WaitForOpend)
                Opened();
        }
    }
}