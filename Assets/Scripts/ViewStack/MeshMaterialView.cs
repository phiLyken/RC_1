using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class VisualStateConfig
{
    public string state_name;
    public Material material;
}

[System.Serializable]
public class VisualState
{
    public VisualState(VisualStateConfig conf)
    {
        material = conf.material;
    }

    public VisualState(string id)
    {
        if (id != "disabled")
        {
            material = TileStateConfigs.GetMaterialForstate(id).material;
        }
       
    }

    public delegate void VisualStateEventHandler(VisualState state);
    public Material material;
    public event VisualStateEventHandler OnRemoveState;

    public void RemoveState()
    {
     
        if (OnRemoveState != null) OnRemoveState(this);
    }

}

public class MeshMaterialView : MonoBehaviour {

    public MeshRenderer mRenderer;
     public  List<VisualState> states;


    public void AddState(VisualState _state)
    {
        if (states == null) states = new List<VisualState>();

       // MDebug.Log("add "+_state.material.name);
        _state.OnRemoveState += s => RemoveState(s);
        states.Add(_state);
        SetTopState();
    }

    public void RemoveState(VisualState _state)
    {
        states.Remove(_state);    
        SetTopState();
    }

    void SetTopState()
    {
        if(mRenderer == null)
        {
            mRenderer = GetComponent<MeshRenderer>();
            if (mRenderer == null) mRenderer = gameObject.AddComponent<MeshRenderer>();
        }


        if (states == null || states.Count == 0)
        {
            mRenderer.material = null;
        } else {
            if(states[states.Count -1].material != null) {
                mRenderer.enabled = true;
                mRenderer.material = states[states.Count - 1].material;
            } else
            {
                mRenderer.enabled = false;
            }
        }
    }

    void OnEnable()
    {
        
        SetTopState();
    }
}

public class MeshViewGroup
{
    List<VisualState> states;

    MeshViewGroup(List<VisualState> _states)
    {
        states = _states;
    }

    public void RemoveGroup()
    {

        for(int i = states.Count-1; i >= 0; i--)
        {
            states[i].RemoveState();
            states.RemoveAt(i);
        }
    }

    public MeshViewGroup(List<Tile> tiles, VisualStateConfig state)
    {
        states = new List<VisualState>();

        foreach(Tile t in tiles)
        {
            if (t == null) continue;
            VisualState s =  new VisualState(state);
            states.Add(s);
            t.SetVisualState(s);
        }
    }

    public MeshViewGroup(List<MeshMaterialView> obj, VisualStateConfig state)
    {
        states = new List<VisualState>();
        foreach (MeshMaterialView view in obj)
        {
            VisualState new_state = new VisualState(state);
            view.AddState(new_state);
            states.Add(new_state);

        }
    }
}