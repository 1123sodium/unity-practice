using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    // using ButtonMap = Dictionary<OVRInput.RawButton, KeyCode>;
    public class ButtonMap
    {
        public KeyCode a;
        public KeyCode b;
        public KeyCode x;
        public KeyCode y;

        public ButtonMap(KeyCode a, KeyCode b, KeyCode x, KeyCode y)
        {
            this.a = a;
            this.b = b;
            this.x = x;
            this.y = y;
        }

        public static ButtonMap defaultValue = new ButtonMap(
            a: KeyCode.A, b: KeyCode.B, x: KeyCode.X, y: KeyCode.Y
        );
    }

    
    public class MyController
    {
        private GameObject rController;
        private ButtonMap buttonMap;

        public MyController(ButtonMap buttonMap = null)
        {
            rController = GameObject.Find("RightHandAnchor");
            if (buttonMap != null)
            {
                this.buttonMap = buttonMap;
            } else
            {
                this.buttonMap = ButtonMap.defaultValue;
            }
        }

        public Vector3 GetRControllerPosition()
        {
            return rController.GetComponent<Transform>().position;
        }

        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }

        public bool GetAButton()
        {
            return OVRInput.Get(OVRInput.RawButton.A) || GetKey(this.buttonMap.a);
        }

        public bool GetBButton()
        {
            return OVRInput.Get(OVRInput.RawButton.B) || GetKey(this.buttonMap.b);
        }

        public bool GetXButton()
        {
            return OVRInput.Get(OVRInput.RawButton.X) || GetKey(this.buttonMap.x);
        }

        public bool GetYButton()
        {
            return OVRInput.Get(OVRInput.RawButton.Y) || GetKey(this.buttonMap.y);
        }

        public Vector2 GetLStick()
        {
            Vector2 ovrStick = OVRInput.Get(OVRInput.RawAxis2D.LThumbstick);
            return ovrStick + GetCursor();
        }

        public Vector2 GetCursor()
        {
            Vector2 cursor = new Vector2(0, 0);
            if (GetKey(KeyCode.UpArrow))
            {
                cursor += new Vector2(0, 1);
            }
            if (GetKey(KeyCode.DownArrow))
            {
                cursor += new Vector2(0, -1);
            }
            if (GetKey(KeyCode.RightArrow))
            {
                cursor += new Vector2(1, 0);
            }
            if (GetKey(KeyCode.LeftArrow))
            {
                cursor += new Vector2(-1, 0);
            }
            return cursor;
        }
    }

}
