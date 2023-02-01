using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float mass;

    public float Mass
    {
        get => mass;
        set => mass = value;
    }

    private float gravityScale;
    
    public float GravityScale
    {
        get => gravityScale;
        set => gravityScale = value;
    }
    
    
    
}
