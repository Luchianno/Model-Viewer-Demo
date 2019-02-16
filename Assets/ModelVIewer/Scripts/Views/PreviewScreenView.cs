using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class PreviewScreenView : BasicView
{
    public UnityEvent BackClicked;
    public UnityEvent ResetClicked;
    public UnityEventInt ToggleChanged;

    [SerializeField]
    List<Toggle> toggles;
    [SerializeField]
    TextMeshProUGUI infoLabel;
    [SerializeField]
    Button back;
    [SerializeField]
    Button reset;

    void Awake()
    {
        back.onClick.AddListener(BackClicked.Invoke);
        reset.onClick.AddListener(ResetClicked.Invoke);
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].onValueChanged.AddListener(x =>
            {
                if (x)
                {
                    OnToggleChanged();
                }
            });
        }
    }

    public void SetInfo(string info) => infoLabel.text = info;

    public void ChangeMesh(int vertices, int polygons)
    {
        infoLabel.text = $"Vertices: {vertices:N1}\nPolygons: {polygons:N1}";
    }

    private void OnToggleChanged()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (toggles[i].isOn)
                ToggleChanged.Invoke(i);
        }

    }
}
