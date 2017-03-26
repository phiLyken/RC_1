using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FooBar : MonoBehaviour {

    public float ReceiveDamageIntAddChance;
    public float AttackIntAddChance;
    public Color IntColor;
    public Color WillColor;
    public Color EmptyColor;
    public Text RestsLeft;
    public int StartWill;
    public int StartInt;
    public int StartRests;
    public int Rests_Current;
    public int Intensity_Current;
    public int Will_Current;

    public int Max;

    public Image BarStep;

    public List<Image> Bar_Steps;

    public bool MaxRached
    {
        get { return Intensity_Current + Will_Current >= Max; }
    }

    public void AddWill(int amount)
    {
        Debug.Log(amount);
        Will_Current = Mathf.Max(Mathf.Min(Will_Current + amount, Max - Intensity_Current),0) ;
        
        UpdateBar();
    }

    public void AddInt(int amount, bool consumeWill)
    {
      
        int truncated = amount;
        if (!consumeWill)
        {
            truncated = Mathf.Min(amount, Max - (Will_Current + Intensity_Current));
        }
        Intensity_Current = Mathf.Min( Mathf.Max(Intensity_Current + truncated, 0), Max);
        
        Will_Current = Mathf.Min(Will_Current, Max - Intensity_Current);

        Will_Current = Mathf.Max(Will_Current, 1);
        

        UpdateBar();
    }
    public void ReceiveDamage(int amount)
    {
        //  AddInt(  (int)(ReceiveDamageIntAddChance * amount * Random.value) );
        
        AddWill(-1);
        AddInt(2, false);


    }
    public void Attack()
    {
        int toAdd = Random.value < AttackIntAddChance ? 1 : 0;
        AddInt(toAdd, false);
    }
	
	// Update is called once per frame
	void UpdateBar () {

        if(Bar_Steps != null)
        {
            for(int i = Bar_Steps.Count-1; i >= 0; i--)
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

      
        for(int i = 0; i<Bar_Steps.Count; i++)
        {
            Color color = Color.magenta;
            if(Will_Current == 0)
            {
                color = Color.red;
            } else if(i < Will_Current)
            {
                color = WillColor;
            } else if(i < Intensity_Current+ Will_Current)
            {
                color = IntColor;
            } else
            {
                color = EmptyColor;
            }

            Bar_Steps[i].color = color;
            
        }
    }

    public void Rest()
    {
        if(Rests_Current > 0)
        {
            Rests_Current--;
            RestsLeft.text = Rests_Current.ToString();
            int amount = Intensity_Current;

            AddInt(-amount,false);
            AddWill(Max- Will_Current);
        }

        
    }

    public void IntAttack()
    {
        if(Intensity_Current > 2)
        {
            AddInt(-Intensity_Current, false);
        }
    }

    public void Rage()
    {
        AddInt(1, true);
    }

    void Start()
    {
        Reset();
    }
    public void Reset()
    {
        Intensity_Current = StartInt;
        Will_Current = StartWill;
        Rests_Current = StartRests;
        RestsLeft.text = Rests_Current.ToString();
        UpdateBar();
    }
}
