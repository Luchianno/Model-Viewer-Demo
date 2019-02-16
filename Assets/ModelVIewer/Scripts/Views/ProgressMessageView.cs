using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ProgressMessageView : BasicView
{
    [SerializeField]
    TextMeshProUGUI percentageLabel;
    [SerializeField]
    Image progressBar;

    public void UpdateView(float progress)
    {
        percentageLabel.text = Mathf.RoundToInt(progress).ToString();
        progressBar.fillAmount = progress;
    }
}
