using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class ProgressMessageView : BasicView, ITickable
{
    public float Rate = 360f;
    public bool Animating = false;

    [SerializeField]
    TextMeshProUGUI percentageLabel;
    [SerializeField]
    TextMeshProUGUI pathLabel;
    
    [SerializeField]
    TextMeshProUGUI nameLabel;
    [SerializeField]
    Image progressBar;
    [SerializeField]
    Image progressIndicator;

    public void UpdateView(ModelEntry entry)
    {
        // percentageLabel.text = Mathf.RoundToInt(progress).ToString();
        // progressBar.fillAmount = progress;
        nameLabel.text = entry.Name;
        pathLabel.text = entry.GetFullPath();
    }

    public void Tick()
    {
        if (Animating)
        {
            progressIndicator.rectTransform.Rotate(Vector3.forward * Rate * Time.deltaTime, Space.Self);
        }
    }
}
