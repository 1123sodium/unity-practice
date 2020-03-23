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
    }

    // Update is called once per frame
    void Update()
    {
        MyController controller = new MyController();
        if (controller.GetKey(KeyCode.LeftArrow) || controller.GetAButton() || controller.GetXButton())
        {
            curve.MovePosition(new Vector3(0, 0, 0.1f));
        }
        else if (controller.GetKey(KeyCode.RightArrow) || controller.GetBButton() || controller.GetYButton())
        {
            curve.MovePosition(new Vector3(0, 0, -0.1f));
        }
        Vector2 lStick = controller.GetLStick();
        curve.MovePosition(new Vector3(lStick.x, lStick.y, 0) * 0.1f);
        // transform.position = controller.GetRControllerPosition();
        curve.DrawMesh();
    }
}
