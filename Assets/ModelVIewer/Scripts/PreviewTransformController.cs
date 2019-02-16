using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PreviewTransformController : MonoBehaviour
{
    [Inject]
    DragArea dragArea;

    [Inject]
    IModelLoader loader;

    [Inject]
    PreviewScreenView view;

    public int SwipeAmount = 1;
    public float MinZoom = 0.2f;
    public float MaxZoom = 3f;


    Vector3 currentScale = Vector3.one;

    void Start()
    {
        dragArea.OnSwipeChanged.AddListener(SwipeChanged);
        dragArea.OnPinchChanged.AddListener(PinchChanged);
        dragArea.OnPinchEnded.AddListener(PinchEnded);
        view.ResetClicked.AddListener(ResetClicked);
        // dragArea.OnPinchEnded.AddListener(() => currentScale = target.localScale);
    }

    private void ResetClicked()
    {
        loader.LoadedObject.transform.localScale = Vector3.one;
        loader.LoadedObject.transform.rotation = Quaternion.identity;
        loader.LoadedObject.transform.position = Vector3.zero;
    }

    void PinchChanged(DragArea.PinchInfo pinch)
    {
        loader.LoadedObject.transform.localScale = currentScale * pinch.Amount;
        var clampedZoom = Mathf.Clamp(loader.LoadedObject.transform.localScale.x, MinZoom, MaxZoom);
        loader.LoadedObject.transform.localScale = Vector3.one * clampedZoom;

        loader.LoadedObject.transform.Rotate(0, 0, pinch.AngleDelta, Space.World);
        loader.LoadedObject.transform.Translate(pinch.CenterDelta.x / 5, pinch.CenterDelta.y / 5, 0, Space.Self);

    }

    void PinchEnded()
    {
        var clampedZoom = Mathf.Clamp(loader.LoadedObject.transform.localScale.x, MinZoom, MaxZoom);
        loader.LoadedObject.transform.localScale = Vector3.one * clampedZoom;
        currentScale = loader.LoadedObject.transform.localScale;
    }


    void SwipeChanged(DragArea.SwipeInfo change)
    {
        var x = change.Delta.y / SwipeAmount;
        var y = -change.Delta.x / SwipeAmount;
        // loader.LoadedObject.transform.rotation.SetEulerRotation();
        loader.LoadedObject.transform.Rotate(x, y, 0, Space.World);

        // loader.LoadedObject.transform.Rotate(x, y, 0, Space.World);
    }
}
