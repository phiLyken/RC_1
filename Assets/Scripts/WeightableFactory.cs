using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeightableFactory {

    public static IWeightable GetWeighted(List<IWeightable> WeightableObjects) {

        float totalChance = 0;
        foreach (IWeightable w in WeightableObjects) {
            totalChance += w.Weight;
        }

        float r = Random.value * totalChance;
        float last = 0;

        for (int i = 0; i < WeightableObjects.Count; i++) {
            if (r >= last && r <= last + WeightableObjects[i].Weight) {
                return WeightableObjects[i];
            }
            last += WeightableObjects[i].Weight;
        }

        return null;
    }

    public static List<IWeightable> GetWeighted(List<IWeightable> WeightableObjects, int count){

        List<IWeightable> items = new List<IWeightable>();

        for(int i = 0; i < count && i < WeightableObjects.Count; i++)
        {
            IWeightable item = GetWeighted(WeightableObjects);
            items.Add(item);
            WeightableObjects.Remove(item);
        }

        return items;
    }

    public static void UnitTest(List<IWeightable> WeightableObjects){
		Debug.Log("test: "+WeightableObjects.Count);
		List<IWeightable> Result = new List<IWeightable>();

		for(int i = 0; i< 100; i++) Result.Add(GetWeighted(WeightableObjects ));
	
		int count = 0;
		for(int i = 0; i < WeightableObjects.Count; i++){
			//	Debug.Log( (WeightableObjects[i]).WeightableID+" checking");
			foreach(IWeightable w in Result)if(WeightableObjects[i] == w) count++;
			//Debug.Log( (WeightableObjects[i]).WeightableID+" : "+count);
			count = 0;
		}
	}

}
