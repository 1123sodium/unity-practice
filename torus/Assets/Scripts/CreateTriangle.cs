// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class CreateTriangle : MonoBehaviour
{
    [SerializeField]
    private Material _material;
    private Mesh _mesh;
    private Vector3[] _positions = new Vector3[]
    {
        new Vector3(0, 1, 0),
        new Vector3(1, -1, 0),
        new Vector3(-1, -1, 0)
    };
    private int[] _triangles = new int[] { 0, 1, 2 };
    private Vector3[] _normals = new Vector3[]
    {
        new Vector3(0, 0, -1),
        new Vector3(0, 0, -1),
        new Vector3(0, 0, -1)
    };

    private void Awake()
    {
        _mesh = new Mesh();

        _mesh.vertices = _positions;
        _mesh.triangles = _triangles;
        _mesh.normals = _normals;

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
