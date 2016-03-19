using UnityEngine;
using System.Collections;


[System.Serializable]
public class VisualState
{
    public string state_name;
    public Material material; 
}
public class TileVisualizer : MonoBehaviour {

    MeshRenderer m_renderer;

    public VisualState[] states;

    public void SetState(string id)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if(renderer != null)
          renderer.material =  (Resources.Load("VisualTileState") as GameObject).GetComponent<TileVisualizer>().GetMaterialForstate(id);
    }


    Material GetMaterialForstate(string state)
    {
        foreach (VisualState s in states) if (s.state_name == state) return s.material;

        return null;
    }

}
