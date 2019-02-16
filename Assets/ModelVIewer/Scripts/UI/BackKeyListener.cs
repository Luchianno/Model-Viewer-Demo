using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// making it selectable, so we could change is it interactable or not through canvasGroup on the parent
public class BackKeyListener : Selectable
{
    public UnityEvent BackClicked;

    void Update()
    {
        if (interactable && Input.GetKeyUp(KeyCode.Escape))
        {
            BackClicked.Invoke();
        }
    }
}