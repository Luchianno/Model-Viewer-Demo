using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.UI;

public class MaterialController : IInitializable
{
    [Inject]
    PreviewScreenView view;

    [Inject]
    IModelLoader loader;

    List<WireframeRenderer> renderers = new List<WireframeRenderer>();


    public void Initialize()
    {
        view.ToggleChanged.AddListener(OnToggleChanged);
    }

    public void AddShaders()
    {
        renderers.Clear();
        var list = loader.LoadedObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var item in list)
        {
            renderers.Add(item.gameObject.AddComponent<WireframeRenderer>());
        }
    }

    void OnToggleChanged(int index)
    {
        foreach (var item in renderers)
        {
            item.Shaded = index == 0;
            item.Validate();
        }
    }
}
