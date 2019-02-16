using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PreviewTransformController : MonoBehaviour
{
    [Inject]
    DragArea dragArea;

    [Inject(Id = "PreviewObject")]
    Transform target;

    public int SwipeAmount = 1;
    public float MinZoom = 0.2f;
    public float MaxZoom = 3f;


    Vector3 currentScale = Vector3.one;

    void Start()
    {
        dragArea.OnSwipeChanged.AddListener(SwipeChanged);
        dragArea.OnPinchChanged.AddListener(PinchChanged);
        dragArea.OnPinchEnded.AddListener(PinchEnded);
        // dragArea.OnPinchEnded.AddListener(() => currentScale = target.localScale);
    }

    void PinchChanged(DragArea.PinchInfo pinch)
    {
        target.localScale = currentScale * pinch.Amount;
        var clampedZoom = Mathf.Clamp(target.localScale.x, MinZoom, MaxZoom);
        target.localScale = Vector3.one * clampedZoom;
    }

    void PinchEnded()
    {
        var clampedZoom = Mathf.Clamp(target.localScale.x, MinZoom, MaxZoom);
        target.localScale = Vector3.one * clampedZoom;
        currentScale = target.localScale;
    }


    void SwipeChanged(Vector2 change)
    {
        var x = change.y / SwipeAmount;
        var y = -change.x / SwipeAmount;
        // target.rotation.SetEulerRotation();
        target.Rotate(x, y, 0, Space.World);
    }
}
