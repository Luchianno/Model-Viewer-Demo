using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ListGameState : GameStateWithView
{
    [Inject]
    public void Init(ModelListView view)
    {
        this.uiView = view;
    }
}
