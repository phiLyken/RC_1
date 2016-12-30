using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_ShowUnitSpeech : MonoBehaviour {

    Unit m_Unit;

    public Text TF;
    public GameObject TextPlate;

    public void Init(Unit unit)
    {
        UnitSpeechManager.OnSpeech += CheckSpeech;
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
    void CheckSpeech(Unit u, string[] texts)
    {
        if(u == m_Unit)
        {
            StopAllCoroutines();
            TF.text = texts[0];
            TextPlate.SetActive(true);
            StartCoroutine(MyMath.ExecuteDelayed(texts.Length * 0.05f + 1f, () => TextPlate.SetActive(false)));
        }
    }

    Vector3 GetSpeechPosition()
    {
        return Unit.GetHeadPos(m_Unit) + Vector3.up * 0.25f;
    }
}
