using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModelLoader
{
    void StartLoadingModel(ModelEntry model);

    float Progress { get; }
}
