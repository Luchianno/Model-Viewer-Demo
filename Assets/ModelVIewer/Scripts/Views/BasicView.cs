using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BasicView : MonoBehaviour, IView
{
    [SerializeField]
    ExtendedCanvasGroup canvasGroup;

    public void DisableView()
    {
        canvasGroup.RenderingEnabled = false;
    }

    public void EnableView()
    {
        canvasGroup.RenderingEnabled = true;
    }
}
