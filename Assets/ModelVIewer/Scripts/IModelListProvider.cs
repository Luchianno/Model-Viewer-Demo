using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelListProvider
{
    IEnumerable<ModelEntry> Items { get; }
}
