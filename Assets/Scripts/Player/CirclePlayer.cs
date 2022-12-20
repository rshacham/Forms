using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CirclePlayer : Player
{

    private bool _canDash = true;
    private bool _isDashing = false;
    
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingCoolDown;
    [SerializeField] private float dashingTime;
    private bool oneDash;
    
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        
    }

    public new void FixedUpdate()
    {
        if (!_isDashing)
        {
            base.FixedUpdate();
            return;
        }
        
        var playerSpeed = dashingPower * acceleration;
        _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
    }

    public override void Move(InputAction.CallbackContext context)
    {
        base.Move(context);
        if (!oneDash && context.performed)
        {
            Debug.Log("First");
            oneDash = true;
            return;
        }

        if (oneDash && _canDash && context.performed)
        {
            Debug.Log("Second");
            oneDash = false;
            StartCoroutine(Dash());
        }
    }


    IEnumerator Dash()
    {
        Debug.Log("Dash");
        _isDashing = true;
        CanMove = false;
        _canDash = false;
        var originalGravity = _playerRigidBody.gravityScale;
        _playerRigidBody.gravityScale = 0;
        _playerRigidBody.velocity = new Vector2(transform.localScale.x * dashingPower, 0);
        yield return new WaitForSeconds(dashingTime);

        _isDashing = false;
        oneDash = false;
        _canDash = true;
        _playerRigidBody.gravityScale = originalGravity;
        CanMove = true;
    }
}
