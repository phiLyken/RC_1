using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowUnitSpeech : MonoBehaviour {

    Unit m_Unit;

    public Text TF;
    public GameObject TextPlate;

    public void Init(Unit unit)
    {
        SpeechManager_Unit.OnSpeech += CheckSpeech;
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
        if(m_Unit != null && TextPlate.activeSelf)
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
            StartCoroutine(M_Math.ExecuteDelayed(texts.Length * 0.5f + 2.5f, () => TextPlate.SetActive(false)));
        }
    }

    Vector3 GetSpeechPosition()
    {
        return Unit.GetHeadPos(m_Unit);
    }
}
