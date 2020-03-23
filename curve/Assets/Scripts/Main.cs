using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] Material _material;
    private WriteCurve curve;

    // Start is called before the first frame update
    void Start()
    {
        curve = new WriteCurve(_material);
        curve.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        curve.Update();
    }
}
