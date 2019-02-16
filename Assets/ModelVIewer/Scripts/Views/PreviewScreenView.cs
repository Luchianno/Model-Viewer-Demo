using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class PreviewScreenView : BasicView
{
    public UnityEvent backClicked;
    public UnityEvent resetClicked;
    public UnityEventInt toggleChanged;

    [SerializeField]
    List<Toggle> toggles;
    [SerializeField]
    TextMeshProUGUI infoLabel;
    [SerializeField]
    Button back;
    [SerializeField]
    Button reset;
    // [SerializeField]
    // Button info;

    // [SerializeField]
    // ExtendedCanvasGroup infoBox;

    void Awake()
    {
        back.onClick.AddListener(backClicked.Invoke);
        reset.onClick.AddListener(resetClicked.Invoke);
    }

    void OnToggleChange()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].onValueChanged.AddListener(x => toggleChanged.Invoke(i));
        }
    }

    public void SetInfo(string info) => infoLabel.text = info;

    public void ChangeMesh(int vertices, int polygons)
    {
        infoLabel.text = $"Vertices: {vertices:N1}\nPolygons: {polygons:N1}";
    }
}
