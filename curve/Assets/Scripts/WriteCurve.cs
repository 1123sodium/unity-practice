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
        Write();
        //WriteForDebug();
    }

    void Write()
    {
        float r1 = 3.0f;
        float r2 = 1.0f;
        int longitude = 40;
        int meridian = 20;
        float radius = 0.02f;

        _mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();

        for (int i = 0; i < longitude; i++)
        {
            float t = (float)i / longitude;
            Vector3 centralPoint = Curve(t);
            for (int j = 0; j < meridian; j++)
            {
                float s = (float)j / meridian;
                Vector3 normal = Normal(t, s);
                vertices.Add(centralPoint + radius * normal);
                normals.Add(normal);
            }
        }

        for (int i = 0; i < longitude-2; i++)
        {
            for (int j = 0; j < meridian-2; j++)
            {
                var count = meridian * i + j;
                triangles.Add(count);
                triangles.Add(count + meridian + 2);
                triangles.Add(count + 1);

                triangles.Add(count);
                triangles.Add(count + meridian + 1);
                triangles.Add(count + meridian + 2);
            }
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.normals = normals.ToArray();

        _mesh.RecalculateBounds();
    }

    void WriteForDebug()
    {
        int longitude = 20;
        int meridian = 20;
        for (int i = 0; i < longitude; i++)
        {
            float t0 = (float)i / longitude;
            float t1 = (float)(i + 1) / longitude;
            WritePoint(Curve(t0));
            //WriteLine(Curve(t0), Curve(t1));
            //WriteLine(Curve(t0), Curve(t0) + Tangent(t0));
            //WriteVector(Curve(t0), PrincipalNormal(t0));
            //WriteVector(Curve(t0), Binormal(t0));
            for (int j = 0; j < meridian; j++)
            {
                WriteVector(Curve(t0), Normal(t0, (float)j / meridian));
            }
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

    Vector3 PrincipalNormal(float t)
    {
        float dt = 0.001f;
        Vector3 normal = Tangent(t + dt) - Tangent(t);
        return normal.normalized;
    }

    Vector3 Binormal(float t)
    {
        Vector3 binormal = Vector3.Cross(Tangent(t), PrincipalNormal(t));
        return binormal.normalized;  // 正規化いる？
    }

    Vector3 Normal(float t, float s)
    {
        if (s < 0 || s > 1)
        {
            throw new System.Exception();
        }
        float theta = 2 * Mathf.PI * s;
        float x = Mathf.Cos(theta);
        float y = Mathf.Sin(theta);
        return x * PrincipalNormal(t) + y * Binormal(t);
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
