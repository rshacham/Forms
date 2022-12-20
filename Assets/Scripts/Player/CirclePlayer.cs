using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CirclePlayer : Player
{

    private bool _canDash = true;
    private bool _isDashing = false;
    
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingCoolDown;
    [SerializeField] private float dashingTime;
    
    
    private new void Start()
    {
        base.Start();
    }
}
