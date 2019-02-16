using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Zenject;

public class ModelListItemView : MonoBehaviour
{
    public UnityEvent OnClick;

    [SerializeField]
    TextMeshProUGUI nameLabel;
    [SerializeField]
    TextMeshProUGUI pathLabel;
    [SerializeField]
    Button button;
    [SerializeField]
    Image locationIcon;

    AppSettings settings;

    [Inject]
    public void Construct(AppSettings settings)
    {
        this.settings = settings;
    }

    public void Awake()
    {
        button.onClick.AddListener(OnClick.Invoke);
    }

    public void UpdateView(ModelEntry item)
    {
        nameLabel.text = item.Name;
        pathLabel.text = $"Path: {item.Path}";
        switch (item.Location)
        {
            case ModelEntry.ResourceLocation.StreamingAsset:
                locationIcon.sprite = settings.LocalIcon;
                break;
            case ModelEntry.ResourceLocation.Local:
                locationIcon.sprite = settings.LocalIcon;
                break;
            case ModelEntry.ResourceLocation.Remote:
                locationIcon.sprite = settings.RemoteIcon;
                break;
            default:
                break;
        }
    }

    public class Factory : PlaceholderFactory<ModelListItemView>
    {
    }
}
