using System.Collections;
using System.Collections.Generic;
using AsImpL;
using UnityEngine;
using Zenject;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectImporter))]
public class ModelLoader : MonoBehaviour, IModelLoader
{
    // public float Progress => objectImporter.ImportProgress;
    public UnityEvent ImportingComplete => importingComplete;
    public UnityEventString ImportError => importError;
    public GameObject LoadedObject => loadedModel;

    public Transform Parent;
    public ImportOptions Parameters;

    [SerializeField]
    UnityEvent importingComplete;
    [SerializeField]
    UnityEventString importError;

    GameObject loadedModel;

    ObjectImporter objectImporter;
    [Inject]
    GameStateMachine sm;

    ModelEntry entry;

    protected virtual void Awake()
    {
        objectImporter = GetComponent<ObjectImporter>();
    }


    public void LoadFromStreamingAssets(string name, string path, ImportOptions options)
    {
        objectImporter.ImportModelAsync(name, path, Parent, options, out string error);
        objectImporter.ImportError += OnImportError;
        objectImporter.ImportingComplete += OnImportComplete;
        objectImporter.ImportedModel += OnModelImported;

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
        this.entry = model;
        Destroy(loadedModel); // remove old stuff

        if (objectImporter.ImportModelAsync(name, model.GetFullPath(), Parent, options, out string error))
        {
            objectImporter.ImportError += OnImportError;
            objectImporter.ImportingComplete += OnImportComplete;
            objectImporter.ImportedModel += OnModelImported;
        }
        else
        {
            this.importError.Invoke(error);
        }

        // switch (model.Location)
        // {
        //     case ModelEntry.ResourceLocation.StreamingAsset:
        //         LoadFromStreamingAssets(model.Name, model.Path, options ?? new ImportOptions());
        //         break;
        //     case ModelEntry.ResourceLocation.Local:
        //         LoadFromStreamingAssets(model.Name, model.Path, options ?? new ImportOptions());
        //         break;
        //     case ModelEntry.ResourceLocation.Remote:
        //         LoadRemote(model.Name, model.Path, options ?? new ImportOptions());
        //         break;
        //     default:
        //         throw new NotImplementedException();
        // }
    }

    private void OnImportError(string error) => importError.Invoke(error);

    private void OnImportComplete() => importingComplete.Invoke();

    private void OnModelImported(GameObject obj, string name)
    {
        this.loadedModel = obj;
        // anything else?
    }
}
