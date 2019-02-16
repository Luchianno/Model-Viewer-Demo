using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class PreviewGameState : GameStateWithView
{
    [Inject]
    public void Init(PreviewScreenView view)
    {
        this.uiView = view;
    }

    public override void OnEnter<T>(T args)
    {
        var model = (args as ModelEntry);
        if (model == null)
            throw new ArgumentException();

        base.OnEnter(args);
    }
}
