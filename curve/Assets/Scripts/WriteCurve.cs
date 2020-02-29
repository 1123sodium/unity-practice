// using System;  // Math.Pow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteCurve : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject points;

    [SerializeField]
    private Material _material;
    private Mesh _mesh;

    void Awake()
    {
        var r1 = 3.0f;
        var r2 = 1.0f;
        var n = 20;

        _mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();

        for (int i = 0; i <= n; i++)
        {
            var phi = Mathf.PI * 2.0f * i / n;
            var tr = Mathf.Cos(phi) * r2;
            var y = Mathf.Sin(phi) * r2;

            for (int j = 0; j <= n; j++)
            {
                var theta = 2.0f * Mathf.PI * j / n;
                var x = Mathf.Cos(theta) * (r1 + tr);
                var z = Mathf.Sin(theta) * (r1 + tr);

                vertices.Add(new Vector3(x, y, z));
                normals.Add(new Vector3(tr * Mathf.Cos(theta), y, tr * Mathf.Sin(theta)));
            }
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                var count = (n + 1) * j + i;
                triangles.Add(count);
                triangles.Add(count + n + 2);
                triangles.Add(count + 1);

                triangles.Add(count);
                triangles.Add(count + n + 1);
                triangles.Add(count + n + 2);
            }
        }

        // _mesh.vertices = vertices.ToArray();
        // _mesh.triangles = triangles.ToArray();
        // _mesh.normals = normals.ToArray();

        // _mesh.RecalculateBounds();

        // curve
        int segments = 20;
        float pointSize = 0.1f;
        for (int i=0; i<segments; i++)
        {
            GameObject g = Instantiate(pointPrefab, points.transform);
            g.transform.position = Curve((float)i / n);
            g.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
        }
    }

    Vector3 Curve(float t)
    {
        if (t < 0 || t > 1)
        {
            throw new System.Exception();
        };
        float theta = Mathf.PI * t;
        float r = 1.0f;
        float x = Mathf.Cos(theta) * r;
        float y = Mathf.Sin(theta) * r;
        float z = 0f;
        return new Vector3(x, y, z);
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
