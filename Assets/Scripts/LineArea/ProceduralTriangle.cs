using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class ProceduralTriangle : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        setMeshData();
        createProceduralMesh();
    }

    void setMeshData()
    {
        vertices = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0.5f , 0, Mathf.Sqrt(3.0f) / 2.0f),
            new Vector3(1, 0, 0)};

        triangles = new int[] { 0, 1, 2 };
    }

    void createProceduralMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}