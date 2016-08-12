using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum AttachmentPoints
{
    head, torso, left_hand, right_hand, left_foot, right_foot, back
}

[System.Serializable]
public class AttachmentConfig
{
    public AttachmentPoints AttachmentTarget;
    public GameObject Mesh;
}


public class UnitMesh_Attachment : MonoBehaviour {

    public    List<GameObject> current_attachments;

    public void Init(List<AttachmentConfig> attachments_configs, GameObject unit_mesh)
    {
        if(current_attachments != null)
        {
            foreach(GameObject go in current_attachments)
            {
                DestroyImmediate  (go, false);
            }
        }

        current_attachments = AttachPartsToMesh(attachments_configs, unit_mesh);
    }
    public static string GetBoneForPart(AttachmentPoints part)
    {
        switch (part)
        {
            case AttachmentPoints.head:
                return "humanoid Head";
            case AttachmentPoints.torso:
                return "humanoid Spine1";
            case AttachmentPoints.left_hand:
                return "humanoid L Hand";
            case AttachmentPoints.right_hand:
                return "humanoid R Hand";
            case AttachmentPoints.left_foot:
                return "humanoid L Foot";
            case AttachmentPoints.right_foot:
                return "humanoid R Foot";
            case AttachmentPoints.back:
                return "Attach_Backpack";
          
        }
        return "";
    }

    public static List<GameObject> AttachPartsToMesh(List<AttachmentConfig> parts, GameObject unit_mesh)
    {
        List<GameObject> attached = new List<GameObject>();

        foreach (AttachmentConfig part in parts)
        {
            string bone_name = GetBoneForPart(part.AttachmentTarget);

            GameObject new_part = SpawnObjectToBone(unit_mesh, part.Mesh, bone_name);
            attached.Add(new_part);

        }

        return attached;
    }


    public static Transform GetBone(GameObject target, string bone_name)
    {

        return target.transform.FindDeepChild(bone_name);
       
    }
    public static GameObject SpawnObjectToBone(GameObject target, GameObject prefab, string bone_name)
    {

        Transform bone = GetBone(target, bone_name);

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


/// <summary>
/// http://answers.unity3d.com/questions/799429/transformfindstring-no-longer-finds-grandchild.html
/// </summary>
public static class TransformDeepChildExtension
{
    //Breadth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        var result = aParent.Find(aName);
        if (result != null)
            return result;
        foreach (Transform child in aParent)
        {
            result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }


    /*
    //Depth-first search
    public static Transform FindDeepChild(this Transform aParent, string aName)
    {
        foreach(Transform child in aParent)
        {
            if(child.name == aName )
                return child;
            var result = child.FindDeepChild(aName);
            if (result != null)
                return result;
        }
        return null;
    }
    */
}