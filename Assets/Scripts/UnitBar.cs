using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UnitBar : MonoBehaviour {
    public List<Image> Bar_Steps;
    public int Max;
    public Color IntColor;
    public Color WillColor;
    public Color EmptyColor;

    public Image BarStep;

    void UpdateMax(int max)
    {
        Max = max;
        Debug.Log("Update max " + max);
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
            obj.transform.SetParent(transform);
            Bar_Steps.Add(obj.GetComponent<Image>());
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
    }
}
