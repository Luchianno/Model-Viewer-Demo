using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class LoadingGameState : GameStateWithView
{
    IModelLoader loader;

    [Inject]
    public void Init(ProgressMessageView view, IModelLoader loader)
    {
        this.uiView = view;
        this.loader = loader;
    }

    public override void OnEnter<T>(T args)
    {
        var model = (args as ModelEntry);
        if (model == null)
            throw new ArgumentException();

        // loader.StartLoadModel(model, null);


        base.OnEnter(args);
    }

    public override void Update()
    {
        (uiView as ProgressMessageView).UpdateView(loader.Progress);
    }
}
