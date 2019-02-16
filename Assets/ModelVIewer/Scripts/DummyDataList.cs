using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ModelViewerDemo/Test Data")]
public class DummyDataList : ScriptableObject, IModelListProvider
{
    [SerializeField]
    List<ModelEntry> items;

    public IEnumerable<ModelEntry> Items => items.AsReadOnly();
}
