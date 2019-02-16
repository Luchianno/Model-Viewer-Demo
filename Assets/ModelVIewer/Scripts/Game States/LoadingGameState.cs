using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class LoadingGameState : GameStateWithView
{
    IModelLoader loader;

    [Inject]
    GameStateMachine sm;

    [Inject]
    public void Init(ProgressMessageView view, IModelLoader loader)
    {
        this.uiView = view;
        this.loader = loader;
    }

    public override void OnEnter<T>(T args)
    {
        var entry = (args as ModelEntry);
        if (entry == null)
            throw new ArgumentException();

        loader.ImportingComplete.AddListener(() => sm.ChangeState<PreviewGameState, ModelEntry>(entry));
        loader.ImportError.AddListener(x => sm.ChangeState<ErrorGameState, (ModelEntry, string)>((entry, x)));
        loader.StartLoadingModel(entry);
        (uiView as ProgressMessageView).UpdateView(entry);
        (uiView as ProgressMessageView).Animating = true;

        base.OnEnter(args);
    }

    // public override void Update()
    // {
    //     // (uiView as ProgressMessageView).UpdateView(loader.Progress);
    // }
}
