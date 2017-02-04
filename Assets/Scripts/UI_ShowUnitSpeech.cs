﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowUnitSpeech : MonoBehaviour {

    Unit m_Unit;

    public Text TF;
    public GameObject TextPlate;

    public void Init(Unit unit)
    {
        SpeechMananger_Unit.OnSpeech += CheckSpeech;
        m_Unit = unit;
        UpdatePos();
        TextPlate.SetActive(false);
    }

    void CheckKilled(Unit u)
    {
        if(u== m_Unit)
        {
            Destroy(this);
        }
    }

    void UpdatePos()
    {
        if(m_Unit != null)
        {
            UI_WorldPos worldPos = GetComponent<UI_WorldPos>();
            worldPos.SetWorldPosition(GetSpeechPosition());
        }
    }
    void Update()
    {
        UpdatePos();
    }
    void CheckSpeech(Unit u, string[] texts, string arg)
    {
        if(u == m_Unit)
        {
            StopAllCoroutines();
            TF.text = string.Format(texts[0], arg);
            TextPlate.SetActive(true);
            StartCoroutine(M_Math.ExecuteDelayed(texts.Length * 0.5f + 1f, () => TextPlate.SetActive(false)));
        }
    }

    Vector3 GetSpeechPosition()
    {
        return Unit.GetHeadPos(m_Unit) + Vector3.up * 0.25f;
    }
}