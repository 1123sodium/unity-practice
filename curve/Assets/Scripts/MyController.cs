using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyController
{
    public bool GetKey(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }

    public bool GetAButton()
    {
        return OVRInput.Get(OVRInput.RawButton.A) || GetKey(KeyCode.A);
    }

    public bool GetBButton()
    {
        return OVRInput.Get(OVRInput.RawButton.B) || GetKey(KeyCode.B);
    }

    public bool GetXButton()
    {
        return OVRInput.Get(OVRInput.RawButton.X) || GetKey(KeyCode.X);
    }

    public bool GetYButton()
    {
        return OVRInput.Get(OVRInput.RawButton.Y) || GetKey(KeyCode.Y);
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
