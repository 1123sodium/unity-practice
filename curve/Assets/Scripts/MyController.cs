using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyUtil
{
    using ButtonMapData = Dictionary<OVRInput.RawButton, KeyCode>;
    using Stick2DMapData = Dictionary<Stick2D, KeyCode>;
    using Stick3DMapData = Dictionary<Stick3D, KeyCode>;

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

    public enum Stick2D
    {
        Up, Down, Left, Right
    }

    public class Stick2DMap : Stick2DMapData
    {
        public Stick2DMap(Stick2DMapData data): base(data) { }

        public static Stick2DMap defaultValue = new Stick2DMap( new Stick2DMapData
        {
            { Stick2D.Up, KeyCode.UpArrow },
            { Stick2D.Down, KeyCode.DownArrow },
            { Stick2D.Right, KeyCode.RightArrow },
            { Stick2D.Left, KeyCode.LeftArrow }
        });

        public Vector2 ToVector2()
        {
            Vector2 direction = new Vector2(0, 0);
            if (Input.GetKey(this[Stick2D.Up]))
            {
                direction += new Vector2(0, 1);
            }
            if (Input.GetKey(this[Stick2D.Down]))
            {
                direction += new Vector2(0, -1);
            }
            if (Input.GetKey(this[Stick2D.Right]))
            {
                direction += new Vector2(1, 0);
            }
            if (Input.GetKey(this[Stick2D.Left]))
            {
                direction += new Vector2(-1, 0);
            }
            return direction;
        }
    }

    public enum Stick3D
    {
        Up, Down, Left, Right, Above, Below
    }

    public class Stick3DMap : Stick3DMapData
    {
        public Stick3DMap(Stick3DMapData data): base(data) { }

        public static Stick3DMap defaultValue = new Stick3DMap(new Stick3DMapData
        {
            { Stick3D.Up, KeyCode.I },
            { Stick3D.Down, KeyCode.Comma },
            { Stick3D.Right, KeyCode.L },
            { Stick3D.Left, KeyCode.J },
            { Stick3D.Above, KeyCode.U },
            { Stick3D.Below, KeyCode.M }
        });
            
        public Vector3 ToVector3()
        {
            Vector3 direction = new Vector3(0, 0, 0);
            if (Input.GetKey(this[Stick3D.Up]))
            {
                direction += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(this[Stick3D.Down]))
            {
                direction += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(this[Stick3D.Right]))
            {
                direction += new Vector3(1, 0, 0);
            }
            if (Input.GetKey(this[Stick3D.Left]))
            {
                direction += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(this[Stick3D.Above]))
            {
                direction += new Vector3(0, 0, 1);
            }
            if (Input.GetKey(this[Stick3D.Below]))
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
        private Stick2DMap rStickMap;
        private Stick3DMap rStickMap3D;
        private Vector3 rControllerPosition = new Vector3(0, 0, 0);
        private GameObject cube;

        public MyController(ButtonMap buttonMap = null, Stick2DMap rStickMap = null, Stick3DMap rStickMap3D = null)
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
                this.rStickMap = Stick2DMap.defaultValue;
            }
            if (rStickMap3D != null)
            {
                this.rStickMap3D = rStickMap3D;
            }
            else
            {
                this.rStickMap3D = Stick3DMap.defaultValue;
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
