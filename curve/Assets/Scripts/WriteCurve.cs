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
        for (int i=0; i<segments; i++)
        {
            float t0 = (float)i / segments;
            float t1 = (float)(i + 1) / segments;
            WritePoint(Curve(t0));
            //WriteLine(Curve(t0), Curve(t1));
            //WriteLine(Curve(t0), Curve(t0) + Tangent(t0));
            WriteVector(Curve(t0), Normal(t0));
            WriteVector(Curve(t0), Binormal(t0));
        }
    }

    void WritePoint(Vector3 p)
    {
        float pointSize = 0.1f;
        GameObject g = Instantiate(pointPrefab, points.transform);
        g.transform.position = p;
        g.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
    }

    void WriteLine(Vector3 start, Vector3 end)
    {
        // https://qiita.com/7of9/items/3750d30590e3efcfd389
        float width = 0.02f;
        GameObject line = new GameObject("Line");
        LineRenderer renderer = line.AddComponent<LineRenderer>();
        renderer.SetVertexCount(2);
        renderer.SetWidth(width, width);
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, end);
    }

    void WriteVector(Vector3 start, Vector3 vector)
    {
        float length = 0.2f;
        WriteLine(start, start + length * vector);
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
        float z = 0.5f * r * t;
        return new Vector3(x, y, z);
    }

    Vector3 Tangent(float t)
    {
        float dt = 0.001f;
        Vector3 tangent = Curve(t + dt) - Curve(t);
        return tangent.normalized;
    }

    Vector3 Normal(float t)
    {
        float dt = 0.001f;
        Vector3 normal = Tangent(t + dt) - Tangent(t);
        return normal.normalized;
    }

    Vector3 Binormal(float t)
    {
        Vector3 binormal = Vector3.Cross(Tangent(t), Normal(t));
        return binormal.normalized;  // 正規化いる？
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
