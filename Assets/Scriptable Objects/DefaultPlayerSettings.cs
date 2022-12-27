using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DefaultPlayerSettings : ScriptableObject
{
    public float gravity;
    public float jumpingPower;
    
    public float movementSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;

    public void SetValues(float s, float i, float f)
    {
        gravity = s;
        jumpingPower = i;
        acceleration = f;
    }
}
