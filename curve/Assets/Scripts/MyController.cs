using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    using ButtonMapData = Dictionary<OVRInput.RawButton, KeyCode>;

    public class ButtonMap : ButtonMapData
    {
        public ButtonMap(ButtonMapData data): base(data) { }
       
        /*public static implicit operator ButtonMap(ButtonMapData data)
        {
            return new ButtonMap(data);
        }*/

        public static ButtonMap defaultValue = new ButtonMap( new ButtonMapData{
            { OVRInput.RawButton.A, KeyCode.A },
            { OVRInput.RawButton.B, KeyCode.B },
            { OVRInput.RawButton.X, KeyCode.X },
            { OVRInput.RawButton.Y, KeyCode.Y }
        });
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
            if (Input.GetKey(this.down))
            {
                direction += new Vector2(0, -1);
            }
            if (Input.GetKey(this.right))
            {
                direction += new Vector2(1, 0);
            }
            if (Input.GetKey(this.left))
            {
                direction += new Vector2(-1, 0);
            }
            return direction;
        }
    }

    public class StickMap3D
    {
        public KeyCode up;
        public KeyCode down;
        public KeyCode left;
        public KeyCode right;
        public KeyCode above;
        public KeyCode below;

        public StickMap3D(KeyCode up, KeyCode down, KeyCode left, KeyCode right, KeyCode above, KeyCode below)
        {
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
            this.above = above;
            this.below = below;
        }

        public static StickMap3D defaultValue = new StickMap3D(
            up: KeyCode.I, down: KeyCode.Comma,
            left: KeyCode.J, right: KeyCode.L,
            above: KeyCode.U, below: KeyCode.M
        );

        public Vector3 ToVector3()
        {
            Vector3 direction = new Vector3(0, 0, 0);
            if (Input.GetKey(this.up))
            {
                direction += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(this.down))
            {
                direction += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(this.right))
            {
                direction += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(this.left))
            {
                direction += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(this.above))
            {
                direction += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(this.below))
            {
                direction += new Vector3(0, 0, -1);
            }
            return direction;
        }
    }

    
    public class MyController
    {
        private GameObject rController;
        private ButtonMap buttonMap;
        private StickMap rStickMap;
        private StickMap3D rStickMap3D;
        private Vector3 rControllerPosition = new Vector3(0, 0, 0);
        private GameObject cube;

        public MyController(ButtonMap buttonMap = null, StickMap rStickMap = null, StickMap3D rStickMap3D = null)
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
            if (rStickMap3D != null)
            {
                this.rStickMap3D = rStickMap3D;
            }
            else
            {
                this.rStickMap3D = StickMap3D.defaultValue;
            }
            if (!this.IsOnHeadset())
            {
                this.cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                this.cube.transform.position = this.rControllerPosition;
                this.cube.transform.localScale = new Vector3(1, 1, 1) * 0.3f;
            }
        }

        private bool IsOnHeadset()
        {
            string productName = OVRPlugin.productName;
            if (productName == null || productName == "")
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        public void Update()
        {
            this.rControllerPosition += this.rStickMap3D.ToVector3() * 0.1f;
            if (!this.IsOnHeadset()) {
                this.cube.transform.position = this.rControllerPosition;
            }
        }

        public Vector3 GetRControllerPosition()
        {
            if (this.IsOnHeadset())
            {
                return this.rController.GetComponent<Transform>().position;
            }
            else
            {
                return this.rControllerPosition;
            }
        }

        public Quaternion GetRControllerRotation()
        {
            if (this.IsOnHeadset())
            {
                return this.rController.GetComponent<Transform>().rotation;
            }
            else
            {
                return Quaternion.identity;
            }
        }

        public bool GetButton(OVRInput.RawButton button)
        {
            return OVRInput.Get(button) || Input.GetKey(this.buttonMap[button]);
        }


        public Vector2 GetRStick()
        {
            if (this.IsOnHeadset())
            {
                return OVRInput.Get(OVRInput.RawAxis2D.RThumbstick);
            }
            else
            {
                return this.rStickMap.ToVector2();
            }
        }
    }

}
