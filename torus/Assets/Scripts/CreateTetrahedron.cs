// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTetrahedron : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    private Mesh _mesh;

    void Awake()
    {
        
        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();

        var size = 2;
        var origin = new Vector3(0, 2, 0);

        vertices.Add(origin + new Vector3(0, 0, 0));
        vertices.Add(origin + new Vector3(size, 0, 0));
        vertices.Add(origin + new Vector3(0, size, 0));
        vertices.Add(origin + new Vector3(0, 0, size));

        normals.Add(new Vector3(-1, -1, -1));
        normals.Add(new Vector3(1, 0, 0));
        normals.Add(new Vector3(0, 1, 0));
        normals.Add(new Vector3(0, 0, 1));

        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(1);

        triangles.Add(1);
        triangles.Add(2);
        triangles.Add(3);

        triangles.Add(0);
        triangles.Add(3);
        triangles.Add(2);

        triangles.Add(3);
        triangles.Add(0);
        triangles.Add(1);

        _mesh = new Mesh();

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.normals = normals.ToArray();  // これがないと影がつかない

        _mesh.RecalculateBounds();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.DrawMesh(_mesh, Vector3.zero, Quaternion.identity, _material, 0);
    }
}
