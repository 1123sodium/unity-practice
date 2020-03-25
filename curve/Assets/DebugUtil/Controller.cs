using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OVRInput;

namespace DebugUtil
{


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
