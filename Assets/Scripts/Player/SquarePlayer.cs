using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquarePlayer : Player
{
    # region WallJumping Variables

    private bool _isWallJumping;
    private float _wallJumpingDirection;
    [SerializeField] private float wallJumpingTime;
    private float _wallJumpingCounter;
    [SerializeField] private float wallJumpingDuration;
    [SerializeField] private Vector2 wallJumpingPower;
    # endregion
    
    #region Wall Sliding
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private Transform[] wallCheck;
    private bool _isFacingRight = true;
    #endregion

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
        WallSlide(wallCheck, wallSlidingSpeed);
        WallJump();

        if (!_isWallJumping)
        {
            CheckFlip();
        }
    }

    private new void FixedUpdate()
    {
        if (!IsWallSliding && !_isWallJumping)
        {
            base.FixedUpdate();
        }
    }

    private void WallJump()
    {
        if (IsWallSliding)
        {
            _isWallJumping = false;
            _wallJumpingDirection = -transform.localScale.x;
            _wallJumpingCounter = wallJumpingTime;
            
            CancelInvoke(nameof(StopWallJumping));
        }

        else
        {
            // if (_isWallJumping && !isWallSliding)
            // {
                // if (Input.GetAxis("Horizontal") < 0 == _isFacingRight)
                // {
                //     var playerSpeed = Input.GetAxis("Horizontal") * acceleration;
                //     _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
                // }
            // }
            _wallJumpingCounter -= Time.deltaTime;
        }
    }

    public override void Jump(InputAction.CallbackContext context)
    {
        if (!IsWallSliding)
        {
            base.Jump(context);
            return;
        }
        
        else if (context.started)
        {
            _isWallJumping = true;
            _playerRigidBody.velocity = new Vector2(_wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            _wallJumpingCounter = 0;

            if (Math.Abs(transform.localScale.x - _wallJumpingDirection) > 0.1f) // If player facing direction is not matching the wall jumping direction 
            {
                FlipPlayer();
            }
        
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        _isWallJumping = false;
    }

    private void CheckFlip()
    {
        if (_isFacingRight && _playerRigidBody.velocity.x < 0 ||
            !_isFacingRight && _playerRigidBody.velocity.x > 0)
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
