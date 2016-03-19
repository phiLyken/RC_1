using UnityEngine;
using System.Collections;

[System.Serializable]
public class RegionDefinition
{
    public TileManager Region;
    public int Foo;
}
public class RegionLoader : MonoBehaviour {

    public RegionDefinition[] Regions;

    public static TileManager GetRegion()
    {
        RegionLoader rl = (Resources.Load("RegionDefinitions") as GameObject).GetComponent<RegionLoader>();
        return Instantiate(rl.Regions[Random.Range(0, rl.Regions.Length)].Region ).GetComponent<TileManager>();
    }


}
