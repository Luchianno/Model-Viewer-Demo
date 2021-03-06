﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

public class PreviewGameState : GameStateWithView
{
    CameraZoomController zoomController;

    GameStateMachine sm;

    IModelLoader loader;

    MaterialController materialController;

    [Inject]
    public void Init(PreviewScreenView view,
                    CameraZoomController zoomController,
                    GameStateMachine sm,
                    IModelLoader loader,
                    MaterialController materialController)
    {
        this.uiView = view;
        this.sm = sm;
        this.zoomController = zoomController;
        this.loader = loader;
        this.materialController = materialController;

        view.BackClicked.AddListener(() => sm.ChangeState<ListGameState>());
    }

    public override void OnEnter<T>(T args)
    {
        var model = (args as ModelEntry);
        if (model == null)
            throw new ArgumentException();

        zoomController.Adjust(model);
        var view = this.uiView as PreviewScreenView;

        int vertices = 0;
        int polygons = 0;
        foreach (var item in loader.LoadedObject.GetComponentsInChildren<MeshFilter>())
        {
            vertices += item.mesh.vertexCount;
            polygons += item.mesh.triangles.Length / 3;
        }

        view.ChangeMesh(vertices, polygons);
        materialController.AddShaders();

        base.OnEnter(args);
    }
}
