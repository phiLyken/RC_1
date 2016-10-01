using UnityEngine;
using System.Collections;


public interface ITurn {

	/// <summary>
	/// This event should be called when the turnable wants to resort the turn list
	/// </summary>
	TurnableEventHandler TurnTimeUpdated{
		get;
		set;
	}

    /// <summary>
    /// Each turn has a certain cost, that determines when the the next turn happens - the cost will be applied to the time
    /// </summary>
    /// <returns></returns>
  //  int GetTurnTimeCost();

    /// <summary>
    /// Time used for sorting of all turn items
    /// </summary>
    /// <returns></returns>
    float GetTurnTime();



    int GetCurrentTurnCost();


    /// <summary>
    /// When a turn has ended, the turnsystem uses this to determine the next turn time
    /// </summary>
    void SetNextTurnTime(float delta);

    /// <summary>
    /// Returns whether the turn for this item has been ended
    /// </summary>
    /// <returns></returns>
    bool HasEndedTurn();

    /// <summary>
    /// Called when the turn for THIS item has started
    /// </summary>
    void StartTurn();

    /// <summary>
    /// Called when the turn for THIS item has ended (from the turnsystem)
    /// </summary>
    void EndTurn();

    /// <summary>
    /// Called for each turn of any item (use to decrement time of each item)
    /// </summary>
    void GlobalTurn(int turn);

    /// <summary>
    /// When spawned, turnable should register to TurnSystem.Register
    /// </summary>
    void RegisterTurn();

    /// <summary>
    /// When delted, turnable should unregister to TurnSystem.UnRegister
    /// </summary>
    void UnRegisterTurn();

    /// <summary>
    /// Get an ID to display in UI
    /// </summary>
    string GetID();

    /// <summary>
    /// Each turn needs to have a controller (e.g. player id) so the game can switch states
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    int GetTurnControllerID();

    /// <summary>
    /// Will receive global turns, but should not be registed until active
    /// </summary>
    bool IsActive
    {
        get ;
        set;
    }
    /// <summary>
    /// Turnables should have be able to skip a turn
    /// </summary>
    void SkipTurn();

    /// <summary>
    /// This ID is is used for secondary sorting, best use the ID that is received when registering
    /// </summary>
	int StartOrderID{
		get;
		set;
	}

    Color GetColor();

    Sprite GetIcon();

}
