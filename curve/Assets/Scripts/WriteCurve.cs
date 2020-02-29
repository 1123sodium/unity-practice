// using System;  // Math.Pow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteCurve : MonoBehaviour
{
    public GameObject pointPrefab;
    public GameObject points;

    [SerializeField] private int longitude = 200;
    [SerializeField] private int meridian = 20;
    [SerializeField] private float radius = 0.05f;

    [SerializeField] private Material _material;
    private Mesh _mesh;

    void Awake()
    {
        Write();
        //WriteForDebug();
    }

    void Write()
    {
        _mesh = new Mesh();

        var vertices = new List<Vector3>();
        var triangles = new List<int>();
        var normals = new List<Vector3>();

        for (int i = 0; i <= longitude; i++)
        {
            float t = (float)i / longitude;
            Vector3 centralPoint = Curve(t);
            for (int j = 0; j <= meridian; j++)
            {
                float s = (float)j / meridian;
                Vector3 normal = Normal(t, s);
                vertices.Add(centralPoint + radius * normal);
                normals.Add(normal);
            }
        }

        for (int i = 0; i < longitude; i++)
        {
            for (int j = 0; j < meridian; j++)
            {
                triangles.Add(GetIndex(i, j));
                triangles.Add(GetIndex(i+1, j+1));
                triangles.Add(GetIndex(i, j+1));

                triangles.Add(GetIndex(i, j));
                triangles.Add(GetIndex(i+1, j));
                triangles.Add(GetIndex(i+1, j+1));
            }
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.normals = normals.ToArray();

        _mesh.RecalculateBounds();
    }

    int GetIndex(int i, int j)
    {
        return (meridian + 1) * i + j;
    }

    void WriteForDebug()
    {
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
        float pointSize = 0.03f;
        GameObject g = Instantiate(pointPrefab, points.transform);
        g.transform.position = p;
        g.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
    }

    void WriteLine(Vector3 start, Vector3 end)
    {
        // https://qiita.com/7of9/items/3750d30590e3efcfd389
        float width = 0.01f;
        GameObject line = new GameObject("Line");
        LineRenderer renderer = line.AddComponent<LineRenderer>();
        renderer.SetVertexCount(2);
        renderer.SetWidth(width, width);
        renderer.SetPosition(0, start);
        renderer.SetPosition(1, end);
    }

    void WriteVector(Vector3 start, Vector3 vector)
    {
        float length = 2 * radius;
        WriteLine(start, start + length * vector);
    }

    Vector3 Curve(float t)
    {
        // https://en.wikipedia.org/wiki/Trefoil_knot
        float theta = 2 * Mathf.PI * t;
        float r = 0.5f;
        float x = r * (Mathf.Sin(theta) + 2 * Mathf.Sin(2 * theta));
        float y = r * (Mathf.Cos(theta) - 2 * Mathf.Cos(2 * theta));
        float z = -r * Mathf.Sin(3 * theta);
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
