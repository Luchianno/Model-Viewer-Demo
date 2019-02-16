using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class ErrorMessageView : BasicView
{
    [SerializeField]
    ExtendedCanvasGroup moreInfoPanel;
    [SerializeField]
    Button moreInfo;
    [SerializeField]
    Button back;

    [SerializeField]
    TextMeshProUGUI titleLabel;
    [SerializeField]
    TextMeshProUGUI errorLabel;


    public UnityEvent OnBackClicked;

    protected void Awake()
    {
        moreInfo.onClick.AddListener(() => moreInfoPanel.RenderingEnabled = !moreInfoPanel.RenderingEnabled);
        back.onClick.AddListener(OnBackClicked.Invoke);
    }

    public void UpdateView(ModelEntry model, string error)
    {
        titleLabel.text = model.Name;
        titleLabel.text = error;
    }
}
