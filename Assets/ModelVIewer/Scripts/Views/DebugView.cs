using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;

public class DebugView : MonoBehaviour
{
    public TextMeshProUGUI Label;

    DragArea dragArea;

    [Inject]
    void Init(DragArea area)
    {
        dragArea = area;
        dragArea.OnPinchChanged.AddListener(x => UpdateView());
        dragArea.OnSwipeChanged.AddListener(x => UpdateView());
    }

    void UpdateView()
    {
        Label.text = $"Pinch: {dragArea.Pinch}\nSwipe: {dragArea.Swipe}\nDistance: {dragArea.startDistance}"
                    + $"\nCount:{dragArea.pressStart.Count} {dragArea.currentPos.Count}";
    }
}
