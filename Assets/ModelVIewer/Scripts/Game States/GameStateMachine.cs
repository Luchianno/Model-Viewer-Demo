using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameStateMachine : IInitializable, ITickable
{
    [Inject]
    List<GameState> states;

    GameState currentState;


    public void Initialize()
    {
        Debug.Log("Init SM");
        this.ChangeState<ListGameState>(); // default state
    }

    public void Tick()
    {
        if (currentState != null)
            currentState.Update();
    }


    public void ChangeState<T, V>(V args) where T : GameState  // just extra check during compile time, not to fuck up
    {
        Debug.Log($"Changing to state: {typeof(T)}");
        var state = states.Find(x => x.GetType() == typeof(T));
        if (state == null)
        {
            Debug.LogWarning("State not Found");
            return;
        }

        Debug.Log($"exiting current state: {currentState?.GetType().ToString()}");
        currentState?.OnExit();
        currentState = state;
        Debug.Log($"entering to state: {typeof(T)}");
        state.OnEnter<V>(args);
    }

    public void ChangeState<T>() where T : GameState  // just extra check during compile time, not to fuck up
    {
        ChangeState<T, System.Object>(null);
    }

    // public void ChangeState<T, V>(V args = null) where T : GameState where V : SMArgsEmpty // just extra check during compile time, not to fuck up
    // {
    //     Debug.Log($"Changing to state: {typeof(T)}");
    //     var state = states.Find(x => x.GetType() == typeof(T));
    //     if (state == null)
    //     {
    //         Debug.LogWarning("State not Found");
    //         return;
    //     }

    //     Debug.Log($"exiting current state: {currentState?.GetType().ToString()}");
    //     currentState?.OnExit();
    //     currentState = state;
    //     Debug.Log($"entering to state: {typeof(T)}");
    //     state.OnEnter(args);
    // }


}