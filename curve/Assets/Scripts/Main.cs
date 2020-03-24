using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtil;

public class Main : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Material _material2;
    private MyController controller;
    private WriteCurve curve;
    private WriteCurve curveAtController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!!!");
        this.SetCameraPosition();
        this.controller = new MyController();
        this.curve = new WriteCurve(this._material);
        this.curveAtController = new WriteCurve(this._material2, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.controller.GetAButton() || this.controller.GetXButton())
        {
            this.curve.MovePosition(new Vector3(0, 0, 0.1f));
        }
        else if (this.controller.GetBButton() || this.controller.GetYButton())
        {
            this.curve.MovePosition(new Vector3(0, 0, -0.1f));
        }
        Vector2 rStick = this.controller.GetRStick();
        this.curve.MovePosition(new Vector3(rStick.x, rStick.y, 0) * 0.1f);
        this.curve.DrawMesh();

        // curveAtController
        this.controller.UpdateRStickPosition();
        this.curveAtController.SetPosition(this.controller.GetRControllerPosition());
        this.curveAtController.DrawMesh();
    }

    void SetCameraPosition()
    {
        GameObject camera = GameObject.Find("OVRCameraRig");
        Debug.Log(camera.ToString());
        Vector3 cameraPosition = new Vector3(0, 0, -2);
        camera.transform.position = cameraPosition;
        camera.transform.forward = -cameraPosition;
    }
}
