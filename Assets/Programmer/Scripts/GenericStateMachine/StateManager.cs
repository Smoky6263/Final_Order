using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
{
    protected  Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    protected BaseState<EState> CurrentState;

    protected bool IsTransiotioningState = false;

    private void Start() 
    {
        CurrentState.EnterState();
    }
    private void Update()
    {
        CurrentState.UpdateState();
    }
    private void FixedUpdate()
    {
        EState nextStateKey = CurrentState.GetNextState();

        if (IsTransiotioningState == false && nextStateKey.Equals(CurrentState.StateKey))
            CurrentState.FixedUpdateState();

        else if(IsTransiotioningState == false)
            TransitionToState(nextStateKey);
    }

    public void TransitionToState(EState stateKey)
    {
        IsTransiotioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransiotioningState = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CurrentState.OnTriggerEnter(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        CurrentState.OnTriggerStay(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CurrentState.OnTriggerExit(collision);
    }
}
