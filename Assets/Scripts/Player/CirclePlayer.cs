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

    [Header("Circle Movement")] 
    [SerializeField] private float dashingPower;
    [SerializeField] private float dashingCoolDown;
    [SerializeField] private float dashingTime;
    [SerializeField] private float oneDashFactor;
    private int _dashingDirection = 1;
    private bool oneDash = false;
    #endregion
    
    [Header("Triangle Sounds")]
    [SerializeField] private AudioClip[] jumpSounds;

    private new void Start()
    {
        base.Start();
    }

    public override void Jump(InputAction.CallbackContext context, AudioClip[] sounds = null)
    {
        base.Jump(context, jumpSounds);
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
        var originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0;
        _rb.velocity = new Vector2(transform.localScale.x * dashingPower * _dashingDirection, 0);
        yield return new WaitForSeconds(dashingTime);

        StartCoroutine(ResetDashCoolDown());
        _isDashing = false;
        oneDash = false;
        _rb.gravityScale = originalGravity;
        CanMove = true;
    }

    private void ChangeDashingDirection()
    {
        if (_rb.velocity.x > 0)
        {
            _dashingDirection = 1;
        }

        if (_rb.velocity.x < 0)
        {
            _dashingDirection = -1;
        }
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
