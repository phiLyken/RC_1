using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UI_ActionPreviewIcon : MonoBehaviour {
    
    static List<UI_ActionPreviewIcon> previews;
    public Image m_Image;
    public UI_WorldPos worldPos;

    public static void Disable()
    {
        if(previews != null)
        {
            foreach (var preview in previews)
            {
                if(preview != null && preview.gameObject != null)
                 preview.gameObject.SetActive(false);
            }
        }
    }

    public static void PreviewOnTargets(List<Transform> tr, Sprite icon)
    {
       
        if(previews == null)
        {
            previews = new List<UI_ActionPreviewIcon>();
        }

        while(tr.Count > previews.Count) {

            GameObject newPreview = Instantiate(Resources.Load("ability_target_icon") as GameObject);
            newPreview.transform.SetParent(GameObject.FindGameObjectWithTag("UI_World").transform, false);
            previews.Add(newPreview.GetComponent<UI_ActionPreviewIcon>()  );
        }

        for (int i = 0; i < previews.Count; i++)
        {
            previews[i].gameObject.SetActive(i < tr.Count);

            if(i < tr.Count)
            {
                previews[i].SetPreview(tr[i], icon);
            }
        }   
    }

    public void SetPreview(Transform tr, Sprite icon)
    {
        m_Image.sprite = icon;
        worldPos.SetWorldPosObject(tr);
    }

    void OnDestroy()
    {
        previews = null;
    }

    void Update()
    {
        worldPos.UpdatePos();
    }
}
