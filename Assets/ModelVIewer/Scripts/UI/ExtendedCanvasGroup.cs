using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Wrapper around CanvasGroup
[RequireComponent(typeof(CanvasGroup))]
public class ExtendedCanvasGroup : MonoBehaviour
{
    public bool RenderingEnabled
    {
        get
        {
            return renderingEnabled;
        }
        set
        {
            renderingEnabled = value;

            if (value)
                turnOn();
            else
                turnOff();
        }
    }

    CanvasGroup group;
    [SerializeField]
    [Header("default state")]
    bool renderingEnabled = true;

    protected virtual void Awake()
    {
        // Debug.Log(this.gameObject.name);
        group = GetComponent<CanvasGroup>();
        if (renderingEnabled)
            turnOn();
        else
            turnOff();
    }

    void turnOn()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    void turnOff()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }
}