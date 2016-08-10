using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public enum ArmorParts
{
    head, torso, legs
}

[System.Serializable]
public class ArmorPartConfig
{
    public ArmorParts part;
    public GameObject Mesh;
}

public class ArmorConfig : ScriptableObject {

    public List<ArmorPartConfig> Parts;

    public static string GetBoneForPart(ArmorParts part)
    {
        switch (part)
        {
            case ArmorParts.head:
               return "bone_head";
            case ArmorParts.legs:
                return "bone_leg";
            case ArmorParts.torso:
                return "bone_torso";
        }

        return "";
    }

    public static List<GameObject> AttachArmorToMesh(List<ArmorPartConfig> parts, GameObject mesh)
    {
        List<GameObject> attached = new List<GameObject>();

        foreach(ArmorPartConfig part in parts)
        {
            string bone_name = GetBoneForPart(part.part);

            GameObject new_part = SpawnObjectToBone(mesh, part.Mesh, bone_name);
            attached.Add(new_part);
            
        }

        return attached;
    }

    public static GameObject SpawnObjectToBone(GameObject target, GameObject prefab, string bone_name)
    {
        Transform bone = target.transform.Cast<Transform>()
        .Where(tr => tr.name == bone_name)
        .FirstOrDefault();

        if (bone == null)
        {
            Debug.LogWarning("COULD NOT FIND BONE " + bone_name);
            return null;
        }
        GameObject part_instance = Instantiate(prefab, bone.transform.position, bone.transform.rotation) as GameObject;
        part_instance.transform.SetParent(bone);

        return part_instance;
    }
}
