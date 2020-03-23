using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtil;

public class Main : MonoBehaviour
{
    [SerializeField] Material _material;
    private WriteCurve curve;
    private WriteCurve curveAtController;

    // Start is called before the first frame update
    void Start()
    {
        this.curve = new WriteCurve(this._material);
        this.curveAtController = new WriteCurve(this._material, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        MyController controller = new MyController();
        if (controller.GetAButton() || controller.GetXButton())
        {
            this.curve.MovePosition(new Vector3(0, 0, 0.1f));
        }
        else if (controller.GetBButton() || controller.GetYButton())
        {
            this.curve.MovePosition(new Vector3(0, 0, -0.1f));
        }
        Vector2 rStick = controller.GetRStick();
        this.curve.MovePosition(new Vector3(rStick.x, rStick.y, 0) * 0.1f);
        this.curve.DrawMesh();

        // curveAtController
        this.curveAtController.SetPosition(controller.GetRControllerPosition());
        this.curveAtController.DrawMesh();
    }
}
