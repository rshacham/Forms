using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickUtils : MonoBehaviour
{
    public bool IsConnected(int lenJoystick)
    {
        String[] joysticks = Input.GetJoystickNames();
        if (lenJoystick > 0)
        {
            for (int i = 0; i < lenJoystick; i++)
            {
                if (!joysticks[i].Equals(""))
                {
                    return true;
                }
            }

        }

        return false;
    }
}
