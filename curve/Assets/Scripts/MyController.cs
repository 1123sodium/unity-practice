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
}
