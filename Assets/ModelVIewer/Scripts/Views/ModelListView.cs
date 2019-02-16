using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using System;

public class ModelListView : BasicView
{
    public RectTransform listParent;

    public ItemSelectedEvent OnItemSelected;

    [Inject]
    ModelListItemView.Factory factory;

    public void UpdateView(IEnumerable<ModelEntry> items)
    {
        // while (listParent.childCount != 0)
        // {
        //     Destroy(listParent.GetChild(listParent.childCount - 1).gameObject);
        // }

        foreach (Transform item in listParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            var temp = factory.Create();
            // temp.transform.parent = listParent;
            // temp.transform.localScale = Vector3.one;
            var view = temp.GetComponent<ModelListItemView>();
            view.UpdateView(item);
            view.OnClick.AddListener(() => OnItemSelected.Invoke(item));
        }
    }

    [Serializable]
    public class ItemSelectedEvent : UnityEvent<ModelEntry> { }
}
