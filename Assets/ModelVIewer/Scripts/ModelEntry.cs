using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// 
[Serializable]
public class ModelEntry // TODO: change this silly name
{
    public string Name;
    public string Path;
    public ResourceLocation Location;

    public ModelEntry(string name, string path = "", ResourceLocation location = ResourceLocation.StreamingAsset)
    {
        this.Name = name;
        this.Path = path;
        this.Location = location;
    }

    public string GetFullPath()
    {
        try
        {
            switch (this.Location)
            {
                case ResourceLocation.StreamingAsset:
                    return System.IO.Path.Combine(Application.streamingAssetsPath, Path);
                case ResourceLocation.Local:
                    return System.IO.Path.Combine(Application.persistentDataPath, Path);
                case ResourceLocation.Remote:
                    return System.IO.Path.Combine(Path);
                default:
                    return string.Empty;
            }
        }
        catch (System.Exception)
        {
            Debug.LogError("Problem while creating full path");
            return string.Empty;
        }

    }

    public enum ResourceLocation
    {
        StreamingAsset,
        Local,
        Remote
    }
}
