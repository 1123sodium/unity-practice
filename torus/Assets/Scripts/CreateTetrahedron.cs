// using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTetrahedron : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject balls;

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

        // vertices
        var v0 = origin + new Vector3(0, 0, 0);
        var v1 = origin + new Vector3(size, 0, 0);
        var v2 = origin + new Vector3(0, size, 0);
        var v3 = origin + new Vector3(0, 0, size);

        vertices.Add(v0);
        vertices.Add(v1);
        vertices.Add(v2);
        vertices.Add(v3);

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

        // 点の描画
        var ballSize = 0.5f;
        GameObject g0 = Instantiate(ballPrefab, balls.transform);
        g0.transform.position = v0;
        g0.transform.localScale = new Vector3(ballSize, ballSize, ballSize);

        GameObject g1 = Instantiate(ballPrefab, balls.transform);
        g1.transform.position = v1;
        g1.transform.localScale = new Vector3(ballSize, ballSize, ballSize);

        GameObject g2 = Instantiate(ballPrefab, balls.transform);
        g2.transform.position = v2;
        g2.transform.localScale = new Vector3(ballSize, ballSize, ballSize);

        GameObject g3 = Instantiate(ballPrefab, balls.transform);
        g3.transform.position = v3;
        g3.transform.localScale = new Vector3(ballSize, ballSize, ballSize);
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
