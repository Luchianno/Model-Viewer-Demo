using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraZoomController : MonoBehaviour
{
    [SerializeField]
    new Camera camera;

    [Inject]
    IModelLoader loader;

    void Awake()
    {

    }

    public void Adjust(ModelEntry entry)
    {
        var meshFilters = loader.LoadedObject.GetComponentsInChildren<MeshFilter>();

        float size = 0f;
        foreach (var filter in meshFilters)
        {
            Debug.Log(filter.mesh.bounds.size);
            size = Mathf.Max(filter.mesh.bounds.size.y, size);
        }
        Debug.Log(size);
        var distance = size * 0.5f / Mathf.Tan(camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        Debug.Log(distance);
        this.camera.transform.position = new Vector3(0, size, -distance - 5);
    }
}
