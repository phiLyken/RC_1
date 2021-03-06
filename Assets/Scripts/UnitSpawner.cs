﻿using UnityEngine;
using System.Collections;
using System.Linq;

public class UnitSpawner : MonoBehaviour {

	public int SpawnerGroupID;


	Color[] group_color =
	{
		Color.grey,
		Color.blue,
		Color.cyan,
		Color.green,
		Color.yellow,
        Color.magenta,
        Color.white
	};


	public void SpawnUnit(ScriptableUnitConfig unit_config,M_Math.R_Range range, int group, bool hide_player)
	{
		Unit u = UnitFactory.CreateUnit(unit_config, group, range, hide_player);
		 
		if(u != null) { 
			UnitFactory.SpawnUnit(u, GetComponent<Tile>());
		} else
		{
			Debug.LogWarning("Could not spawn unit");
		}
	}
	void OnDrawGizmos()
	{

		Gizmos.color = group_color[Mathf.Clamp(SpawnerGroupID, 0, group_color.Length - 1)] ;
		Gizmos.DrawWireCube(transform.position , Vector3.one);

		//MyMath.SceneViewText(MyMath.StringArrToLines(Unit_Ids), transform.position + Vector3.up, Color.magenta);
	}
}
