using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IModelLoader
{
    // float Progress { get; }
    UnityEvent ImportingComplete { get; }
    UnityEventString ImportError { get; }
    GameObject LoadedObject { get; }

    void StartLoadingModel(ModelEntry model);
}
