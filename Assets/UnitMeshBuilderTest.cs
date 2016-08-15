using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMeshBuilderTest : MonoBehaviour {

    List<SkinnedMeshRenderer> current;

    public UnitMeshConfig MeshConfig;

    void Start()
    {
        Init(MeshConfig);
    }

    public void Init(UnitMeshConfig conf)
    {
       if(current != null)
        {
           List<SkinnedMeshRenderer> to_remove = new List<SkinnedMeshRenderer>();

            foreach(SkinnedMeshRenderer rend in current)
            {
                Destroy(rend.gameObject);             
            }          
        }

        current = UnitFactory.SpawnSkinnedMeshToUnit(this.gameObject, MeshConfig.Head, MeshConfig.Suit);
    }


}

[System.Serializable]
public class UnitMeshConfig
{
    public GameObject Head;

    public GameObject Suit;
}
