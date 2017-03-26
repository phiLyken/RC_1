using UnityEngine;
using System.Collections;

public abstract class UI_ToolTip_Base : MonoBehaviour {

    bool Hovered;
    public EventHandler OnHide;
    UI_ToolTip_Base child;
    public abstract void SetItem(object obj);

    public void SetMouseOver()
    {
        Hovered = true;
       // MDebug.Log("hovered");
    }

    public void SetMouseExit()
    {
       // MDebug.Log("exit");
        Hovered = false;
        AttemptHide();
    }
    public bool AttemptHide()
    {

       // MDebug.Log("Attempt Hide " + gameObject + "  hover:" + Hovered);
        if (!Hovered && (child == null || !child.gameObject.activeSelf))
        {
            gameObject.SetActive(false);

            if (OnHide != null)
                 OnHide();
                
             return true;
      
        }

        return false;
    }

    IEnumerator DelayedAttemptHide()
    {

        yield return null;
        AttemptHide();
   
    }
    void ChildHidden()
    {
        StartCoroutine(DelayedAttemptHide());
    }
    public void RegisterChild(UI_ToolTip_Base ch)
    {
        child = ch;
        child.OnHide += ChildHidden;
    }
   public void Show( )
    {
       // if (_parent != null)
       //     MDebug.Log("FOUND PARENT");

      
      //  this.parent = _parent;
        gameObject.SetActive(true);
    }
}
