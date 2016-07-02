using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewGroupTest : MonoBehaviour {

    public VisualState m_state;
    public VisualState startState;
    // Update is called once per frame

    public List<MeshMaterialView> objects;

    public MeshViewGroup group;
    void Awake() { 

        GetComponent<MeshMaterialView>().AddState(startState);
        GetComponent<MeshMaterialView>().AddState(new VisualState(TileStateConfigs.GetMaterialForstate("normal")));
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            m_state = new VisualState(TileStateConfigs.GetMaterialForstate("editor_selected"));
            GetComponent<MeshMaterialView>().AddState(m_state);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            m_state.RemoveState();
        }

                if (Input.GetKeyUp(KeyCode.D))
        {
            GetComponent<MeshMaterialView>().AddState(startState);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            group = new MeshViewGroup(objects, TileStateConfigs.GetMaterialForstate("editor_selected"));
        }

        if (Input.GetKeyDown(KeyCode.L)) group.RemoveGroup();
	}

    
}
