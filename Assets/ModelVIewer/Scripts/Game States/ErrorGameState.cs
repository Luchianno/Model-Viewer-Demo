using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class ErrorGameState : GameStateWithView
{
    [Inject]
    GameStateMachine sm;

    [Inject]
    public void Init(ErrorMessageView view)
    {
        this.uiView = view;
        view.OnBackClicked.AddListener(() => sm.ChangeState<ListGameState>());
    }

    public override void OnEnter<T>(T args)
    {
        var model = args as (ModelEntry, string)?;
        if (model == null)
            throw new ArgumentException();

        (uiView as ErrorMessageView).UpdateView(model.Value.Item1, model.Value.Item2);

        base.OnEnter(args);
    }

}
