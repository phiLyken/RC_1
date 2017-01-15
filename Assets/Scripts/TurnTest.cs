using UnityEngine;
using System.Collections;
using System;

public class TurnTest : MonoBehaviour, ITurn {
	public string id;
	public int ownerid;

	public int[] ActionCost;
	int selectedAction = -1;

 
	Action<ITurn> UpdateCostPreview;

	float currentTurnTime;
	int actionsMade;
	int currentTurnCost;

	public bool active;

	// Use this for initialization
	void Start () {
		RegisterTurn();
        actionsMade = 2;
	}

	// Update is called once per frame
	void Update () {
		if(TurnSystem.HasTurn(this)){

			if(Input.GetKeyUp(KeyCode.Escape)){
				SelectAction(-1);
			}
			if(Input.GetKeyUp(KeyCode.Alpha1)){
				SelectAction(0);
			}
			if(Input.GetKeyUp(KeyCode.Alpha2)){
				SelectAction(1);
			}
			if(Input.GetKeyUp(KeyCode.Alpha3)){
				SelectAction(2);
			}
			if(Input.GetKeyUp(KeyCode.Alpha4)){
				SelectAction(3);
			}
			if(Input.GetKeyUp(KeyCode.Alpha5)){
				SelectAction(4);
			}
		}
	}

	#region ITurn implementation

	public Action<ITurn> TurnTimeUpdated 
	{
		get { return UpdateCostPreview;}
		set { UpdateCostPreview = value;}
	}


    public int GetCurrentTurnCost()
    {
        int turnTime = 0;

        if (selectedAction >= 0)
        {
         
            turnTime += ActionCost[selectedAction];
        }
        turnTime += currentTurnCost;
      
        return turnTime;
    }
	void SelectAction	(int id){
		
		Debug.Log("attempt select "+id  +" length"+ActionCost.Length);

		if(id == -1){
			Debug.Log("select "+id);
			selectedAction = -1;
		} else if(id < ActionCost.Length && id >= 0){
			Debug.Log("asdasd");
			 if (selectedAction == id){
				currentTurnCost += ActionCost[id];
				actionsMade ++;
				selectedAction = -1;
				Debug.Log("execte "+id+"   actions:"+actionsMade);
			} else {
				selectedAction = id;
			}
		}

		if(UpdateCostPreview != null) UpdateCostPreview(this);

	}
	public float GetTurnTime() { 

        return   currentTurnTime;
       
	}

	public void SetNextTurnTime (float delta)
	{
		currentTurnTime += delta;
	}

	public bool HasEndedTurn ()
	{
		return actionsMade == 2;
	}

    public Color GetColor()
    {
        return Color.magenta;
    }
	public void StartTurn ()
	{
         
		Debug.Log("Start Turn "+GetID());
		selectedAction = -1;
		actionsMade = 0;
		currentTurnCost = 0;
	}
    public void EndTurn()
    {
        currentTurnCost = 0;
    }
	public void GlobalTurn (int turn)
	{
		currentTurnTime--;
	}


	public void UnRegisterTurn ()
	{
		TurnSystem.Unregister(this);
	}

	public string GetID ()
	{
		return id+" time:"+GetTurnTime()+"  current:"+GetCurrentTurnCost();
	}

	public int GetTurnControllerID ()
	{
		return ownerid;
	}

	public void SkipTurn ()
	{
		actionsMade = 2;
	}


    public bool IsActive {
		get {
			return active;
		}
        set
        {
            active = value;
        }
	}

	public void RemoveEntity(){
		UnRegisterTurn();
	}

	public int StartOrderID {
		get {
			return	starting_order;
		}
		set {
			starting_order = value;
		}
	}

 

    public void RegisterTurn()
	{
		starting_order =   TurnSystem.Register(this);
	}

    public Sprite GetIcon()
    {
        return null;
    }

    int starting_order;

    public event System.Action OnUpdateSprite;
    #endregion
}
