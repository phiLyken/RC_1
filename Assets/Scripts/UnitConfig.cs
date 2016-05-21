using UnityEngine;
using System.Collections;

[System.Serializable]
public enum StatType
{
	complex,simple
}
[System.Serializable]
public class UnitConfig 
{
	public string ID;
	public StatType StatType;
	public StatInfo[] stats;   

	public UnitActionBase[] Actions;

	public int Owner;
	public int TriggerRange;
	
	public GameObject Mesh;

	public int UnitPower;
}

