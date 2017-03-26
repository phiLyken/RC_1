using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Objective_View : GenericView<Objective>
{

    public Text TitleTF;
    public Text DescrTF;
    public GameObject CompletedToggle;

    public event Action OnRemoved;

    public bool RemoveOnComplete;
    
    protected override void OnSet(Objective item)
    {
       

        m_Item.OnComplete += Updated;
        m_Item.OnComplete += Remove;

        if(RemoveOnComplete)
            m_Item.OnComplete += Remove;
        // MDebug.Log("Set Objective in View");

        Show();
    }
    public override void Remove()
    {
        m_Item.OnCancel -= Remove;
        m_Item.OnComplete -= Updated;


        StartCoroutine(M_Math.ExecuteDelayed(1.5f, Removed));
        
     
    }   
    void Removed()
    {
        StartCoroutine(M_Extensions.YieldT((f) => { gameObject.SetUIAlpha(1 - f); }, 0.5f));
        StartCoroutine(M_Math.ExecuteDelayed(0.5f, () => { OnRemoved.AttemptCall(); Destroy(this.gameObject); }));
    }
    void Show()
    {
        gameObject.SetUIAlpha(0);
        StartCoroutine(M_Math.ExecuteDelayed( m_Item.GetIndex() == 0 ? 0.5f : 4f,
            () => StartCoroutine(M_Extensions.YieldT((f) => { gameObject.SetUIAlpha(f); }, 0.5f))
        ));
    }

    public override void Updated()
    {
        TitleTF.text = m_Item.Config.Title;
        CompletedToggle.SetActive(m_Item.GetComplete());
        DescrTF.text = m_Item.Config.Description;
    }

 
}

