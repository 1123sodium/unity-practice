// using System;  // Math.Pow
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteCurve // : MonoBehaviour
{
    private int longitude = 200;
    private int meridian = 20;
    private float radius = 0.05f;

    private Material _material;
    private Mesh _mesh;

    private Vector3 position = new Vector3(0, 0, 0);

    public WriteCurve(Material material)
    {
        _material = material;
    }
    
    public void Awake()
    {
        Write();
        //WriteForDebug();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        MyController controller = new MyController();
        if (controller.GetKey(KeyCode.LeftArrow) || controller.GetAButton() || controller.GetXButton()) {
            position += new Vector3(0, 0, 0.1f);
        } else if (controller.GetKey(KeyCode.RightArrow) || controller.GetBButton() || controller.GetYButton()) {
            position -= new Vector3(0, 0, 0.1f);
        }
        Vector2 lStick = controller.GetLStick();
        position += new Vector3(lStick.x, lStick.y, 0) * 0.1f;
        // transform.position = controller.GetRControllerPosition();
        Graphics.DrawMesh(_mesh, position, Quaternion.identity, _material, 0);
    }

    Vector3 Curve(float t)
    {
        // 0 <= t <= 1 + epsilon
        // 微分の計算をする都合で、1より少しだけ大きな値も入ってくる (回避できるけど、その必要ある？)
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
        float dt = 0.1f / longitude;
        Vector3 tangent = Curve(t + dt) - Curve(t);
        return tangent.normalized;
    }

    Vector3 PrincipalNormal(float t)
    {
        float dt = 0.1f / longitude;
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
                triangles.Add(GetIndex(i, j+1));
                triangles.Add(GetIndex(i+1, j+1));

                triangles.Add(GetIndex(i, j));
                triangles.Add(GetIndex(i+1, j+1));
                triangles.Add(GetIndex(i+1, j));
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
}
