using UnityEngine;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

public class TurnSystemMockData : MonoBehaviour {

    public TurnSystemMock[] MockDataStart;

    public List<ITurn> turnables;

    void Awake()
    {
        turnables = GetMockTurnList();
    }

    public List<ITurn> GetMockTurnList()
    {
        return MockDataStart.Cast<ITurn>().ToList();
    }
    [System.Serializable]
    public class TurnSystemMock : ITurn
    {
        TurnableEventHandler onUpdate;
        public Color color;
        public int turnTime;
        public string ID;
        int _order;

        public TurnableEventHandler TurnTimeUpdated
        {
            get { return onUpdate; }
            set { onUpdate = value; }
        }

        void OnTurnTimeUpdate(ITurn t)
        {

        }
        public bool IsActive
        {
          get
            {
                return true;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int StartOrderID
        {
            get
            {
                return _order;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int GetTurnTime()
        {
            return turnTime;
        }

        public int GetCurrentTurnCost()
        {
            return UnityEngine.Random.Range(5, 10);
        }

        public void SetNextTurnTime(int turns)
        {
            turnTime = turns;
        }

        public bool HasEndedTurn()
        {
            return Input.GetButtonDown("Fire1");
        }

        public void StartTurn()
        {
            Debug.Log("Start Turn");
        }

        public void EndTurn()
        {
            Debug.Log("End Turn");
        }

        public void GlobalTurn(int turn)
        {
            throw new NotImplementedException();
        }

        public void RegisterTurn()
        {
            _order = TurnSystem.Register(this);
        }

        public void UnRegisterTurn()
        {
            throw new NotImplementedException();
        }

        public string GetID()
        {
            return ID;
        }

        public int GetTurnControllerID()
        {
            return 0;
        }

        public void SkipTurn()
        {
            throw new NotImplementedException();
        }

        public Color GetColor()
        {
            return color;
        }
    }
}
