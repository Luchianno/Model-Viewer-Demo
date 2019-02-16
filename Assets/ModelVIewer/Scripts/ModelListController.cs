using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using AsImpL;
using System.IO;
using System;
using UnityEngine.Networking;
using System.Collections.ObjectModel;

public class ModelListController : IInitializable, IDisposable
{
    public ReadOnlyCollection<ModelEntry> ModelList { get { return modelList.AsReadOnly(); } }

    // move this to settings
    const string listPath = "modelList";

    [Inject]
    GameStateMachine sm;

    [Inject]
    List<IModelListProvider> listProviders; // in case of multiple list providers
    // IModelListProvider listProvider;


    [Inject]
    ModelListView view;

    List<ModelEntry> modelList;

    public void Initialize()
    {
        modelList = new List<ModelEntry>();
        foreach (var listProvider in listProviders) // in case of multiple list providers
        {
            modelList.AddRange(listProvider.Items);
        }

        view.OnItemSelected.AddListener(ModelSelected);
        view.UpdateView(modelList);
    }

    public void ModelSelected(ModelEntry entry)
    {
        sm.ChangeState<LoadingGameState, ModelEntry>(entry);
    }

    public void LoadFromPersistantPath()
    {
        var fullPath = Path.Combine(Application.persistentDataPath, listPath);

        string list = string.Empty;
        if (File.Exists(fullPath))
        {
            list = File.ReadAllText(fullPath);
            try
            {
                modelList = JsonUtility.FromJson<List<ModelEntry>>(list);
                Debug.Log($"{modelList.Count} models found in list");
            }
            catch (System.Exception)
            {
                Debug.Log("can't deserialize json");
            }
        }
        else
        {
            modelList = new List<ModelEntry>();
        }
    }

    public void Add(string name, string path)
    {
        // modelList.Add(new MeshItem() { Name = name, Path = path });
        // ImportOptions options = new ImportOptions();

        // objectImporter.ImportModelAsync(name, path, )
    }

    public void Dispose()
    {
        File.WriteAllText(Path.Combine(Application.persistentDataPath, listPath), JsonUtility.ToJson(this.modelList, true));
    }
}
