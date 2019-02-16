using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PreviewRenderController : MonoBehaviour
{
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    MeshFilter meshFilter;
    new Camera camera;

    void Awake()
    {
        camera = GetComponent<Camera>();
    }

    Texture2D RenderPreview(Mesh mesh, Material mat)
    {


        var currentRT = RenderTexture.active;
        RenderTexture.active = camera.targetTexture;

        camera.Render();

        var image = new Texture2D(camera.targetTexture.width, camera.targetTexture.height);
        image.ReadPixels(new Rect(0, 0, camera.targetTexture.width, camera.targetTexture.height), 0, 0);
        image.Apply();

        RenderTexture.active = currentRT;
        return image;
    }
}
