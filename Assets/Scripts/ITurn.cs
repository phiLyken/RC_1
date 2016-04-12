using UnityEngine;
using System.Collections;


public interface ITurn {

	/// <summary>
	/// This event should be called when the turnable is changing the turnorder
	/// </summary>
	TurnableEvent OnTurnPreview{
		get;
		set;
	}

    /// <summary>
    /// Each turn has a certain cost, that determines when the the next turn happens - the cost will be applied to the time
    /// </summary>
    /// <returns></returns>
    int GetTurnTimeCost();

    /// <summary>
    /// Time used for sorting of all turn items
    /// </summary>
    /// <returns></returns>
    int GetNextTurnTime();

    /// <summary>
    /// When a turn has ended, the turnsystem uses this to determine the next turn time
    /// </summary>
    void SetNextTurnTime(int turns);

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


    bool IsActive
    {
        get ;
    }

    void SkipTurn();

	int StartOrderID{
		get;
		set;
	}
}
