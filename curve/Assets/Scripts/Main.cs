using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DebugUtil;

public class Main : MonoBehaviour
{
    [SerializeField] Material _material;
    [SerializeField] Material _material2;
    private Controller controller;
    private WriteCurve curve;
    private WriteCurve curveAtController;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start!!!");
        Debug.Log(OVRPlugin.productName);
        this.SetCameraPosition();
        this.SetUpController();
        this.curve = new WriteCurve(this._material);
        this.curveAtController = new WriteCurve(this._material2, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.controller.GetButton(OVRInput.RawButton.A) || this.controller.GetButton(OVRInput.RawButton.X))
        {
            this.curve.MovePosition(new Vector3(0, 0, 0.1f));
        }
        else if (this.controller.GetButton(OVRInput.RawButton.B) || this.controller.GetButton(OVRInput.RawButton.Y))
        {
            this.curve.MovePosition(new Vector3(0, 0, -0.1f));
        }
        Vector2 rStick = this.controller.rightHand.GetStick();
        this.curve.MovePosition(new Vector3(rStick.x, rStick.y, 0) * 0.1f);
        this.curve.DrawMesh();

        // curveAtController
        this.controller.Update();
        this.curveAtController.SetPosition(this.controller.rightHand.GetPosition());
        this.curveAtController.SetRotation(this.controller.rightHand.GetRotation());
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

    void SetUpController()
    {
        this.controller = new Controller(
            buttonMap: ButtonMap.PositionalKeys,
            rightStickMap: Stick2DMap.Arrows,
            rightHandMover: Stick3DMap.OKLSemiIComma,
            leftHandMover: Stick3DMap.WASDEC
        );
    }
}
