using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PreviewScreenView : BasicView
{
    [SerializeField]
    List<Toggle> toggles;
    [SerializeField]
    MeshFilter meshFilter;
    [SerializeField]
    MeshRenderer meshRenderer;
    [SerializeField]
    TextMeshProUGUI infoLabel;
    [SerializeField]
    List<Material> materials;

    void OnToggleChange()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].onValueChanged.AddListener(x =>
            {
                if (x)
                    ChangeMaterial(materials[i]);
            });
        }
    }

    public void ChangeMaterial(Material material)
    {
        this.meshRenderer.material = material;
    }

    public void ChangeMesh(Mesh mesh)
    {
        this.meshFilter.mesh = mesh;
        infoLabel.text = $"Vertices: {mesh.vertexCount}\nTriangles: {mesh.triangles.Length / 3}";
    }
}
