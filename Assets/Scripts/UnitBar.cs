using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class UnitBar : MonoBehaviour {

    public RectTransform AdrenalineRushPreview;
    public Image AdrenalineRushPreview_Adr;
    public List<Image> Bar_Steps;
    public int Max;
    public Color IntColor;
    public Color WillColor;
    public Color EmptyColor;

    public Image BarStep;

    void UpdateMax(int max)
    {
        Max = max;
       // Debug.Log("Update max " + max);
        if (Bar_Steps != null)
        {
            for (int i = Bar_Steps.Count - 1; i >= 0; i--)
            {
                Destroy(Bar_Steps[i].gameObject);
                Bar_Steps.RemoveAt(i);
            }
        }

        Bar_Steps = new List<Image>();
        BarStep.gameObject.SetActive(true);
        for (int i = 0; i < Max; i++)
        {
            GameObject obj = (Instantiate(BarStep.gameObject) as GameObject);

            obj.transform.SetParent(transform, false);
            Bar_Steps.Add(obj.GetComponent<Image>());
		//	obj.GetComponent<RectTransform>().sizeDelta = new Vector2(10,10);
        }

        BarStep.gameObject.SetActive(false);
    }

    public  void SetBarValues(int _will, int _intensity, int _max)
    {
        Debug.Log(_will + "  " + _intensity + " " + _max);
        if(_max != Max)
        {
            UpdateMax(_max);
        }
		//Debug.Log(Bar_Steps.Count);
        for (int i = 0; i < Bar_Steps.Count; i++)
        {
            Color color = Color.magenta;
            if (_will == 0)
            {
                color = Color.red;
            }
            else if (i < _will)
            {
                color = WillColor;
            }
            else if (i < _intensity + _will)
            {
                color = IntColor;
            }
            else
            {
                color = EmptyColor;
            }

            Bar_Steps[i].color = color;

        }

        int preview_pos = Mathf.Min(_will, _max - 2);
        SetAdrenalineRushPreview(preview_pos, _intensity);
    }

    void SetAdrenalineRushPreview(int at_nunmber, int adrenaline_count)
    {
        if(adrenaline_count > 1)
        {
            AdrenalineRushPreview.gameObject.SetActive(false);
        } else
        {
            AdrenalineRushPreview.gameObject.SetActive(true);
            RectTransform target = Bar_Steps[at_nunmber].rectTransform;
            AdrenalineRushPreview.SetParent(target, false);
        
            AdrenalineRushPreview.anchoredPosition = new Vector3(target.sizeDelta.x / 2, -target.sizeDelta.y / 2, 0);
            AdrenalineRushPreview_Adr.gameObject.SetActive(adrenaline_count == 1);
        }
    }
}
