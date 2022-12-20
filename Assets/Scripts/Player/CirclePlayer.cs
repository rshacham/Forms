using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CirclePlayer : Player
{

    private bool _canDash = true;
    private bool _isDashing = false;
    
    #region Dashing
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingCoolDown;
    [SerializeField] private float dashingTime;
    [SerializeField] private float oneDashFactor;
    private bool oneDash = false;
    #endregion
    
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
        Debug.Log("Can Dash = " + _canDash);
        if (!oneDash && context.performed)
        {
            StopCoroutine(ResetOneDash());
            Debug.Log("First Dash");
            oneDash = true;
            StartCoroutine(ResetOneDash());
            return;
        }

        if (oneDash && _canDash && context.performed)
        {
            oneDash = false;
            StartCoroutine(Dash());
        }
    }


    private IEnumerator Dash()
    {
        Debug.Log("Dash");
        _isDashing = true;
        CanMove = false;
        _canDash = false;
        var originalGravity = _playerRigidBody.gravityScale;
        _playerRigidBody.gravityScale = 0;
        _playerRigidBody.velocity = new Vector2(transform.localScale.x * dashingPower, 0);
        yield return new WaitForSeconds(dashingTime);

        StartCoroutine(ResetDashCoolDown());
        _isDashing = false;
        oneDash = false;
        _playerRigidBody.gravityScale = originalGravity;
        CanMove = true;
    }

    private IEnumerator ResetDashCoolDown()
    {
        yield return new WaitForSeconds(dashingCoolDown);
        _canDash = true;
    }

    private IEnumerator ResetOneDash()
    {
        yield return new WaitForSeconds(oneDashFactor);
        oneDash = false;
    }
}
