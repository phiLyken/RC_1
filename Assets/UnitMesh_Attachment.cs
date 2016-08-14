using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum AttachmentPoints
{
   left_hand, right_hand
}

[System.Serializable]
public class AttachmentConfig
{
    public AttachmentPoints AttachmentTarget;
    public GameObject Mesh;
}


public class UnitMesh_Attachment : MonoBehaviour {
    
    public static string GetBoneForPart(AttachmentPoints part)
    {
        switch (part)
        {

            case AttachmentPoints.left_hand:
                return "Attach_Lhand";
            case AttachmentPoints.right_hand:
                return "Attach_Rhand";
            
          
        }
        return "";
    }


    public static Transform GetBone(GameObject target, string bone_name)
    {
        return target.transform.FindDeepChild(bone_name);
       
    }
    public static void AttachToBone(GameObject target_rig, GameObject part, AttachmentPoints point)
    {

        string bone_name = GetBoneForPart(point);
        Transform bone = GetBone(target_rig, bone_name);

        if (bone == null)
        {
            Debug.LogWarning("COULD NOT FIND BONE " + bone_name);
         
        }
        //  GameObject part_instance = Instantiate(part, bone.transform.position, bone.transform.rotation) as GameObject;
        part.transform.rotation = bone.transform.rotation;
        part.transform.position = bone.transform.position;
        part.transform.SetParent(bone);

       
    }
}

