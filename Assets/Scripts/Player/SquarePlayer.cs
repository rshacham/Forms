using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquarePlayer : Player
{
    # region WallJumping Variables
    [Header("Square Movement")]
    private bool _isWallJumping;
    private float _wallJumpingDirection;
    [SerializeField] private float wallJumpingTime;
    private float _wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingPower;
    [SerializeField] private float wallJumpUpForceFactor;
    # endregion
    
    [Header("Square Sliding")]
    #region Wall Sliding
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private Transform[] wallCheck;
    private bool _isFacingRight = true;
    private bool _isWalled = false;
    [SerializeField] private float wallStickDuration;
    private float _onWallTimer;
    #endregion

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        CheckFlip();
    }
    
    private bool IsWalled(IEnumerable<Transform> wallCheck)
    {
        foreach (var checker in wallCheck)
        {
            if (Physics2D.OverlapCircle(checker.position, 0.5f, wallLayer))
            {
                return true;
            }
        }

        return false;
    }
    protected void WallSlide(IEnumerable<Transform> wallCheck, float wallSlidingSpeed)
    {
        if (IsWalled(wallCheck) && !IsGrounded)
        {
            IsWallSliding = true;
        }

        else
        {
            IsWallSliding = false;
        }
    }
    private new void FixedUpdate()
    {
        WallSlide(wallCheck, wallSlidingSpeed);
        BasicMovement();

        if (IsWallSliding)
        {
            SquareWallSlide();
        }
    }

    private void SquareWallSlide()
    {
        _rb.gravityScale = _gravityScale;
    }

    private new void UpdateFallGravity()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _gravityScale * fallGravityMultiplier;
        }

        else
        {
            _rb.gravityScale = _gravityScale;
        }
    }


    private void WallJump()
    {
        if (IsWallSliding)
        {
            GameManager.Manager.HasWallJumped = true;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _wallJumpingDirection = -transform.localScale.x;
            var upForce = Mathf.Abs(_rb.velocity.y) * wallJumpUpForceFactor + wallJumpingPower.y;
            _rb.AddForce(new Vector2(wallJumpingPower.x * _wallJumpingDirection, upForce), ForceMode2D.Impulse);
        }

        else
        {
            _wallJumpingCounter -= Time.deltaTime;
        }
    }

    public override void Jump(InputAction.CallbackContext context)
    {
        if (!IsWallSliding)
        {
            base.Jump(context);
        }
        
        else if (context.started)
        {
            WallJump();

            if (Math.Abs(transform.localScale.x - _wallJumpingDirection) > 0.1f) // If player facing direction is not matching the wall jumping direction 
            {
                FlipPlayer();
            }
        }
    }

    private void StopWallJumping()
    {
        _isWallJumping = false;
    }

    private void CheckFlip()
    {
        if (_isFacingRight && _rb.velocity.x < -0.1 ||
            !_isFacingRight && _rb.velocity.x > 0.1)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
