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

    public class StickMap
    {
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;

        public StickMap(KeyCode up, KeyCode down, KeyCode left, KeyCode right)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public static StickMap defaultValue = new StickMap(
            up: KeyCode.UpArrow, down: KeyCode.DownArrow,
            left: KeyCode.LeftArrow, right: KeyCode.RightArrow
        );

        public Vector2 ToVector2()
        {
            Vector2 direction = new Vector2(0, 0);
            if (Input.GetKey(this.up))
            {
                direction += new Vector2(0, 1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                direction += new Vector2(0, -1);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction += new Vector2(1, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction += new Vector2(-1, 0);
            }
            return direction;
        }
    }

    
    public class MyController
    {
        private GameObject rController;
        private ButtonMap buttonMap;
        private StickMap rStickMap;

        public MyController(ButtonMap buttonMap = null, StickMap rStickMap = null)
        {
            rController = GameObject.Find("RightHandAnchor");
            if (buttonMap != null)
            {
                this.buttonMap = buttonMap;
            } 
            else
            {
                this.buttonMap = ButtonMap.defaultValue;
            }
            if (rStickMap != null)
            {
                this.rStickMap = rStickMap;
            }
            else
            {
                this.rStickMap = StickMap.defaultValue;
            }
        }

        public Vector3 GetRControllerPosition()
        {
            if (OVRPlugin.productName != null)
            {
                return rController.GetComponent<Transform>().position;
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
            
        }

        /*
        public bool GetKey(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }
        */

        public bool GetAButton()
        {
            return OVRInput.Get(OVRInput.RawButton.A) || Input.GetKey(this.buttonMap.a);
        }

        public bool GetBButton()
        {
            return OVRInput.Get(OVRInput.RawButton.B) || Input.GetKey(this.buttonMap.b);
        }

        public bool GetXButton()
        {
            return OVRInput.Get(OVRInput.RawButton.X) || Input.GetKey(this.buttonMap.x);
        }

        public bool GetYButton()
        {
            return OVRInput.Get(OVRInput.RawButton.Y) || Input.GetKey(this.buttonMap.y);
        }

        public Vector2 GetRStick()
        {
            Vector2 ovrStick = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
            return ovrStick + this.rStickMap.ToVector2();
        }
    }

}
