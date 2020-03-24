﻿using System.Collections;
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

        public static ButtonMap LiteralKeys = new ButtonMap( new ButtonMapData{
            { OVRInput.RawButton.A, KeyCode.A },
            { OVRInput.RawButton.B, KeyCode.B },
            { OVRInput.RawButton.X, KeyCode.X },
            { OVRInput.RawButton.Y, KeyCode.Y },
            { OVRInput.RawButton.RIndexTrigger, KeyCode.R },
            { OVRInput.RawButton.LIndexTrigger, KeyCode.L }
        });

        public static ButtonMap PositionalKeys = new ButtonMap(new ButtonMapData{
            { OVRInput.RawButton.A, KeyCode.Comma },
            { OVRInput.RawButton.B, KeyCode.Period },
            { OVRInput.RawButton.X, KeyCode.X },
            { OVRInput.RawButton.Y, KeyCode.Z },
            { OVRInput.RawButton.RIndexTrigger, KeyCode.P },
            { OVRInput.RawButton.LIndexTrigger, KeyCode.Q }
        });
    }
    
    public enum Stick2D
    {
        Up, Down, Left, Right
    }

    public class Stick2DMap : Stick2DMapData
    {
        public Stick2DMap(Stick2DMapData data): base(data) { }

        public static Stick2DMap Arrows = new Stick2DMap( new Stick2DMapData
        {
            { Stick2D.Up, KeyCode.UpArrow },
            { Stick2D.Down, KeyCode.DownArrow },
            { Stick2D.Right, KeyCode.RightArrow },
            { Stick2D.Left, KeyCode.LeftArrow }
        });

        public static Stick2DMap WASD = new Stick2DMap( new Stick2DMapData
        {
            { Stick2D.Up, KeyCode.W },
            { Stick2D.Down, KeyCode.S },
            { Stick2D.Right, KeyCode.D },
            { Stick2D.Left, KeyCode.A }
        });

        /*
        public static Stick2DMap ijkl = new Stick2DMap(new Stick2DMapData {
            { Stick2D.Up, KeyCode.I },
            { Stick2D.Down, KeyCode.K },
            { Stick2D.Right, KeyCode.L },
            { Stick2D.Left, KeyCode.J }
        });
        */

        public static Stick2DMap OKLSemi = new Stick2DMap(new Stick2DMapData {
            { Stick2D.Up, KeyCode.O },
            { Stick2D.Down, KeyCode.L },
            { Stick2D.Right, KeyCode.K },
            { Stick2D.Left, KeyCode.Comma }
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

        public static Stick3DMap WASDEC = new Stick3DMap(new Stick3DMapData
        {
            { Stick3D.Up, KeyCode.W },
            { Stick3D.Down, KeyCode.S },
            { Stick3D.Right, KeyCode.D },
            { Stick3D.Left, KeyCode.A },
            { Stick3D.Above, KeyCode.E },
            { Stick3D.Below, KeyCode.C }
        });

        /*
        public static Stick3DMap ijklum = new Stick3DMap(new Stick3DMapData
        {
            { Stick3D.Up, KeyCode.I },
            { Stick3D.Down, KeyCode.K },
            { Stick3D.Right, KeyCode.L },
            { Stick3D.Left, KeyCode.J },
            { Stick3D.Above, KeyCode.U },
            { Stick3D.Below, KeyCode.M }
        });
        */

        public static Stick3DMap OKLSemiIComma = new Stick3DMap(new Stick3DMapData
        {
            { Stick3D.Up, KeyCode.O },
            { Stick3D.Down, KeyCode.L },
            { Stick3D.Right, KeyCode.Semicolon },
            { Stick3D.Left, KeyCode.K },
            { Stick3D.Above, KeyCode.I },
            { Stick3D.Below, KeyCode.Comma }
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

    public class VRController
    {
        private string handAnchorName; // warning の表示に使うだけ
        private GameObject handAnchor;
        private OVRInput.RawAxis2D stick;
        private Stick2DMap stickMap;
        private Stick3DMap positionMover;
        private Vector3 position = new Vector3(0, 0, 0);
        private GameObject cube;
        private bool onHeadSet;

        public VRController(string handAnchorName, OVRInput.RawAxis2D stick, Stick2DMap stickMap, Stick3DMap positionMover, bool onHeadSet)
        {
            this.handAnchor = GameObject.Find(handAnchorName);
            this.stick = stick;
            this.stickMap = stickMap;
            this.positionMover = positionMover;
            this.onHeadSet = onHeadSet;

            if (!onHeadSet)
            {
                this.cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                this.cube.transform.position = this.position;
                this.cube.transform.localScale = new Vector3(1, 1, 1) * 0.3f;
            }
        }
        
        public void Update()
        {
            if (!this.onHeadSet)
            {
                if (this.positionMover != null)
                {
                    this.position += this.positionMover.ToVector3() * 0.1f;
                }
                this.cube.transform.position = this.position;
            }
        }

        public Vector2 GetStick()
        {
            if (this.onHeadSet)
            {
                return OVRInput.Get(this.stick);
            }
            else
            {
                if (this.stickMap == null)
                {
                    Debug.LogWarning($"VRController.stickMap ({this.handAnchorName}) is null");
                    return new Vector2(0, 0);
                }
                else
                {
                    return this.stickMap.ToVector2();
                }
            }
        }

        public Vector3 GetPosition()
        {
            if (this.onHeadSet)
            {
                return this.handAnchor.GetComponent<Transform>().position;
            }
            else
            {
                return this.position;
            }
        }

        public Quaternion GetRotation()
        {
            if (this.onHeadSet)
            {
                return this.handAnchor.GetComponent<Transform>().rotation;
            }
            else
            {
                Debug.Log("GetRotation is not suppported on non-VRheadset environment");
                return Quaternion.identity;
            }
        }
    }



    public class MyController
    {
        private ButtonMap buttonMap;
        public VRController rController;
        public VRController lController;

        public MyController(
            ButtonMap buttonMap = null, 
            Stick2DMap rStickMap = null,
            Stick3DMap rControllerMover = null,
            Stick2DMap lStickMap = null,
            Stick3DMap lControllerMover = null
        )
        {
            this.buttonMap = buttonMap;
            this.rController = new VRController(
                handAnchorName: "RightHandAnchor",
                stick: OVRInput.RawAxis2D.RThumbstick,
                stickMap: rStickMap,
                positionMover: rControllerMover,
                this.IsOnHeadset()
            );
            this.lController = new VRController(
                handAnchorName: "LeftHandAnchor",
                stick: OVRInput.RawAxis2D.LThumbstick,
                stickMap: lStickMap,
                positionMover: lControllerMover,
                this.IsOnHeadset()
            );
        }

        private bool IsOnHeadset()
        {
            string productName = OVRPlugin.productName;
            return !(productName == null || productName == "");
        }

        public void Update()
        {
            this.rController.Update();
            this.lController.Update();
        }

        public bool GetButton(OVRInput.RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return OVRInput.Get(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                if (!this.buttonMap.ContainsKey(button))
                {
                    Debug.LogWarning($"MyController.buttonMap does not contain the key {button}");
                    return false;
                }
                return Input.GetKey(this.buttonMap[button]);
            }
        }
        public bool GetButtonDown(OVRInput.RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return OVRInput.GetDown(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                if (!this.buttonMap.ContainsKey(button))
                {
                    Debug.LogWarning($"MyController.buttonMap does not contain the key {button}");
                    return false;
                }
                return Input.GetKeyDown(this.buttonMap[button]);
            }
        }
        public bool GetButtonUp(OVRInput.RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return OVRInput.GetUp(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                if (!this.buttonMap.ContainsKey(button))
                {
                    Debug.LogWarning($"MyController.buttonMap does not contain the key {button}");
                    return false;
                }
                return Input.GetKeyUp(this.buttonMap[button]);
            }
        }
    }
 }
