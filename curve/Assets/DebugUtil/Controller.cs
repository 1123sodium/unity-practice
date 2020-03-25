using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static OVRInput;

namespace DebugUtil
{

    using ButtonMapData = List<(RawButton button, KeyCode key)>;
    using Stick2DMapData = List<(Stick2D stickDirection, KeyCode key)>;
    using Stick3DMapData = List<(Stick3D stickDirection, KeyCode key)>;

    public class ButtonMap // : ButtonMapData
    {
        private Dictionary<RawButton, List<KeyCode>> mappedKeys = new Dictionary<RawButton, List<KeyCode>> { };
        public ButtonMap(ButtonMapData data) {
            foreach (var map in data)
            {
                if (this.mappedKeys.ContainsKey(map.button))
                {
                    this.mappedKeys[map.button].Add(map.key);
                }
                else
                {
                    this.mappedKeys[map.button] = new List<KeyCode> { map.key };
                }
            }
        }

        /*public static implicit operator ButtonMap(ButtonMapData data)
        {
            return new ButtonMap(data);
        }*/

        public static ButtonMap LiteralKeys = new ButtonMap( new ButtonMapData{
            ( RawButton.A, KeyCode.A ),
            ( RawButton.B, KeyCode.B ),
            ( RawButton.X, KeyCode.X ),
            ( RawButton.Y, KeyCode.Y ),
            ( RawButton.RIndexTrigger, KeyCode.R ),
            ( RawButton.LIndexTrigger, KeyCode.L )
        });

        public static ButtonMap PositionalKeys = new ButtonMap(new ButtonMapData{
            ( RawButton.A, KeyCode.Period ),
            ( RawButton.B, KeyCode.Slash ),
            ( RawButton.X, KeyCode.X ),
            ( RawButton.Y, KeyCode.Z ),
            ( RawButton.RIndexTrigger, KeyCode.P ),
            ( RawButton.LIndexTrigger, KeyCode.Q )
        });

        public bool Get(RawButton button)
        {
            if (!this.mappedKeys.ContainsKey(button)) 
            {
                return false;
            }
            return this.mappedKeys[button].Any(key => Input.GetKey(key));
        }

        public bool GetDown(RawButton button)
        {
            if (!this.mappedKeys.ContainsKey(button))
            {
                return false;
            }
            return this.mappedKeys[button].Any(key => Input.GetKeyDown(key));
        }

        public bool GetUp(RawButton button)
        {
            if (!this.mappedKeys.ContainsKey(button))
            {
                return false;
            }
            return this.mappedKeys[button].Any(key => Input.GetKeyUp(key));
        }
    }
    
    public enum Stick2D
    {
        Up, Down, Left, Right
    }

    public class Stick2DMap // : Stick2DMapData
    {
        private Dictionary<Stick2D, List<KeyCode>> mappedKeys = new Dictionary<Stick2D, List<KeyCode>> { };
        public Stick2DMap(Stick2DMapData data) { 
            foreach (var map in data)
            {
                if (this.mappedKeys.ContainsKey(map.stickDirection))
                {
                    this.mappedKeys[map.stickDirection].Add(map.key);
                }
                else
                {
                    this.mappedKeys[map.stickDirection] = new List<KeyCode> { map.key };
                }
            }
        }

        public static Stick2DMap Arrows = new Stick2DMap( new Stick2DMapData
        {
            ( Stick2D.Up, KeyCode.UpArrow ),
            ( Stick2D.Down, KeyCode.DownArrow ),
            ( Stick2D.Right, KeyCode.RightArrow ),
            ( Stick2D.Left, KeyCode.LeftArrow )
        });

        public static Stick2DMap WASD = new Stick2DMap( new Stick2DMapData
        {
            ( Stick2D.Up, KeyCode.W ),
            ( Stick2D.Down, KeyCode.S ),
            ( Stick2D.Right, KeyCode.D ),
            ( Stick2D.Left, KeyCode.A )
        });

        /*
        public static Stick2DMap ijkl = new Stick2DMap(new Stick2DMapData {
            ( Stick2D.Up, KeyCode.I ),
            ( Stick2D.Down, KeyCode.K ),
            ( Stick2D.Right, KeyCode.L ),
            ( Stick2D.Left, KeyCode.J )
        });
        */

        public static Stick2DMap OKLSemi = new Stick2DMap(new Stick2DMapData {
            ( Stick2D.Up, KeyCode.O ),
            ( Stick2D.Down, KeyCode.L ),
            ( Stick2D.Right, KeyCode.K ),
            ( Stick2D.Left, KeyCode.Comma )
        });

        public bool Get(Stick2D stickDirection)
        {
            if (!this.mappedKeys.ContainsKey(stickDirection))
            {
                return false;
            }
            return this.mappedKeys[stickDirection].Any(key => Input.GetKey(key));
        }

        public Vector2 ToVector2()
        {
            Vector2 direction = new Vector2(0, 0);
            if (this.Get(Stick2D.Up))
            {
                direction += new Vector2(0, 1);
            }
            if (this.Get(Stick2D.Down))
            {
                direction += new Vector2(0, -1);
            }
            if (this.Get(Stick2D.Right))
            {
                direction += new Vector2(1, 0);
            }
            if (this.Get(Stick2D.Left))
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

    public class Stick3DMap // : Stick3DMapData
    {
        private Dictionary<Stick3D, List<KeyCode>> mappedKeys = new Dictionary<Stick3D, List<KeyCode>> { };
        public Stick3DMap(Stick3DMapData data)
        {
            foreach (var map in data)
            {
                if (this.mappedKeys.ContainsKey(map.stickDirection))
                {
                    this.mappedKeys[map.stickDirection].Add(map.key);
                }
                else
                {
                    this.mappedKeys[map.stickDirection] = new List<KeyCode> { map.key };
                }
            }
        }

        public static Stick3DMap WASDEC = new Stick3DMap(new Stick3DMapData
        {
            ( Stick3D.Up, KeyCode.W ),
            ( Stick3D.Down, KeyCode.S ),
            ( Stick3D.Right, KeyCode.D ),
            ( Stick3D.Left, KeyCode.A ),
            ( Stick3D.Above, KeyCode.E ),
            ( Stick3D.Below, KeyCode.C )
        });

        /*
        public static Stick3DMap ijklum = new Stick3DMap(new Stick3DMapData
        {
            ( Stick3D.Up, KeyCode.I ),
            ( Stick3D.Down, KeyCode.K ),
            ( Stick3D.Right, KeyCode.L ),
            ( Stick3D.Left, KeyCode.J ),
            ( Stick3D.Above, KeyCode.U ),
            ( Stick3D.Below, KeyCode.M )
        });
        */

        public static Stick3DMap OKLSemiIComma = new Stick3DMap(new Stick3DMapData
        {
            ( Stick3D.Up, KeyCode.O ),
            ( Stick3D.Down, KeyCode.L ),
            ( Stick3D.Right, KeyCode.Semicolon ),
            ( Stick3D.Left, KeyCode.K ),
            ( Stick3D.Above, KeyCode.I ),
            ( Stick3D.Below, KeyCode.Comma )
        });

        public bool Get(Stick3D stickDirection)
        {
            if (!this.mappedKeys.ContainsKey(stickDirection))
            {
                return false;
            }
            return this.mappedKeys[stickDirection].Any(key => Input.GetKey(key));
        }

        public Vector3 ToVector3()
        {
            Vector3 direction = new Vector3(0, 0, 0);
            if (this.Get(Stick3D.Up))
            {
                direction += new Vector3(0, 1, 0);
            }
            if (this.Get(Stick3D.Down))
            {
                direction += new Vector3(0, -1, 0);
            }
            if (this.Get(Stick3D.Right))
            {
                direction += new Vector3(1, 0, 0);
            }
            if (this.Get(Stick3D.Left))
            {
                direction += new Vector3(-1, 0, 0);
            }
            if (this.Get(Stick3D.Above))
            {
                direction += new Vector3(0, 0, 1);
            }
            if (this.Get(Stick3D.Below))
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
        private RawAxis2D stick;
        private Stick2DMap stickMap;
        private Stick3DMap positionMover;
        private Vector3 position = new Vector3(0, 0, 0);
        private GameObject cube;
        private bool onHeadSet;

        public VRController(string handAnchorName, RawAxis2D stick, Stick2DMap stickMap, Stick3DMap positionMover, bool onHeadSet)
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
                return Get(this.stick);
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



    public class Controller
    {
        private ButtonMap buttonMap;
        public VRController rController;
        public VRController lController;

        public Controller(
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
                stick: RawAxis2D.RThumbstick,
                stickMap: rStickMap,
                positionMover: rControllerMover,
                this.IsOnHeadset()
            );
            this.lController = new VRController(
                handAnchorName: "LeftHandAnchor",
                stick: RawAxis2D.LThumbstick,
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

        public bool GetButton(RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return Get(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                return this.buttonMap.Get(button);
            }
        }
        public bool GetButtonDown(RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return GetDown(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                return this.buttonMap.GetDown(button);
            }
        }
        public bool GetButtonUp(RawButton button)
        {
            if (this.IsOnHeadset())
            {
                return GetUp(button);
            }
            else
            {
                if (this.buttonMap == null)
                {
                    return false;
                }
                return this.buttonMap.GetUp(button);
            }
        }
    }
 }
