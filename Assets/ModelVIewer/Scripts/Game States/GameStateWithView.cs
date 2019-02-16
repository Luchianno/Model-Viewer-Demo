using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateWithView : GameState
{
    protected IView uiView;

    public override void Initialize()
    {
        // Debug.Log($"Init {this.GetType()}");
        uiView.DisableView();
    }

    public override void OnEnter<T>(T args)
    {
        uiView.EnableView();
    }

    public override void OnExit()
    {
        uiView.DisableView();
    }
}
