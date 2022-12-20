using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CirclePlayer : Player
{
    #region Dashing
    private bool _canDash = true;
    private bool _isDashing = false;

    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingCoolDown;
    [SerializeField] private float dashingTime;
    [SerializeField] private float oneDashFactor;
    private int _dashingDirection = 1;
    private bool oneDash = false;
    #endregion
    
    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        ChangeDashingDirection();
    }

    private void OnEnable()
    {
        StartCoroutine(ResetDashCoolDown());
        oneDash = false;
    }

    public new void FixedUpdate()
    {
        if (!_isDashing)
        {
            base.FixedUpdate();
            return;
        }
        
        // var playerSpeed = dashingPower * acceleration;
        // _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
    }

    public override void Move(InputAction.CallbackContext context)
    {
        base.Move(context);
        if (!oneDash && context.performed)
        {
            oneDash = true;
            StopCoroutine(ResetOneDash());
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
        _isDashing = true;
        CanMove = false;
        _canDash = false;
        var originalGravity = _playerRigidBody.gravityScale;
        _playerRigidBody.gravityScale = 0;
        _playerRigidBody.velocity = new Vector2(transform.localScale.x * dashingPower * _dashingDirection, 0);
        yield return new WaitForSeconds(dashingTime);

        StartCoroutine(ResetDashCoolDown());
        _isDashing = false;
        oneDash = false;
        _playerRigidBody.gravityScale = originalGravity;
        CanMove = true;
    }

    private void ChangeDashingDirection()
    {
        if (_playerRigidBody.velocity.x > 0)
        {
            _dashingDirection = 1;
        }

        if (_playerRigidBody.velocity.x < 0)
        {
            _dashingDirection = -1;
        }
    }

    private IEnumerator ResetDashCoolDown()
    {
        yield return new WaitForSeconds(dashingCoolDown);
        _canDash = true;
        Debug.Log("Can Move Again");
    }

    private IEnumerator ResetOneDash()
    {
        yield return new WaitForSeconds(oneDashFactor);
        oneDash = false;
    }
}
