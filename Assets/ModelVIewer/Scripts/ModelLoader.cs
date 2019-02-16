using System.Collections;
using System.Collections.Generic;
using AsImpL;
using UnityEngine;
using Zenject;
using System;

[RequireComponent(typeof(ObjectImporter))]
public class ModelLoader : MonoBehaviour//, IModelLoader
{
    public Transform Parent;
    public ImportOptions Parameters;

    GameObject loadedModel;

    [Inject]
    ObjectImporter objectImporter;

    protected virtual void Awake()
    {
        objectImporter = GetComponent<ObjectImporter>();

        objectImporter.ImportError += OnImportError;
        objectImporter.ImportingComplete += OnImportComplete;
    }

    private void OnImportError(string error)
    {
        throw new NotImplementedException();
    }

    private void OnImportComplete()
    {
        throw new NotImplementedException();
    }

    public void LoadFromStreamingAssets(string name, string path, ImportOptions options)
    {
        objectImporter.ImportModelAsync(name, path, Parent, options);

        // objectImporter.ImportModelAsync("lala", Application.streamingAssetsPath + "/face2.obj", null, new ImportOptions());
        // a.ImportedModel(ModelImported)
        // UnityWebRequest. .LoadFromCacheOrDownload()
    }

    public void StartLoadingModel(ModelEntry model)
    {
        StartLoadingModel(model, Parameters);
    }

    public void StartLoadingModel(ModelEntry model, ImportOptions options)
    {
        Destroy(loadedModel); // remove old stuff
        switch (model.Location)
        {
            case ModelEntry.ResourceLocation.StreamingAsset:
                LoadFromStreamingAssets(model.Name, model.Path, options ?? new ImportOptions());
                break;
            case ModelEntry.ResourceLocation.Local:
                LoadFromStreamingAssets(model.Name, model.Path, options ?? new ImportOptions());
                break;
            case ModelEntry.ResourceLocation.Remote:
                LoadFromStreamingAssets(model.Name, model.Path, options ?? new ImportOptions());
                break;
            default:
                throw new NotImplementedException();
        }
    }

}
