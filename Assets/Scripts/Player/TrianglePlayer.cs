using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrianglePlayer : Player
{
    # region Rotation
    [SerializeField] private float startWalkRotationBoost;
    [SerializeField] private float walkingRotationSpeed;
    private bool startedWalking = false;
    [SerializeField] private float airFactor;
    private float rotationSpeed;
    # endregion
    
    private bool _canDoubleJump = true;

    #region Wall Sliding
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private Transform[] wallCheck;
    private bool _isFacingRight = true;
    #endregion

    public new void Update()
    {
        base.Update();
        UpdateRotationSpeed();
        if (IsGrounded)
        {
            _canDoubleJump = true;
        }
    }

    public new void FixedUpdate()
    {
        if (!IsWallSliding)
        {
            if (_canMove)
            {
                BasicMovement();
                if (Mathf.Abs(_movementInput.x) > 0.1f && CheckIfStuck() && IsGrounded)
                {
                    StartWalkJump();
                }
            }
        }

        if (Mathf.Abs(_movementInput.x) < 0.1f)
        {
            startedWalking = false;
        }
        
        PlayerRigidBody.AddTorque(rotationSpeed * Time.fixedDeltaTime * Mathf.Sign(_movementInput.x) * -1);
    }

    public bool CheckIfStuck()
    {
        return (Math.Abs(transform.eulerAngles.z - 240) < 0.3f
                || Math.Abs(transform.eulerAngles.z - 120) < 0.3f
                || Math.Abs(transform.eulerAngles.z - 0) < 0.3f);
    }
    public override void Jump(InputAction.CallbackContext context)
    {
        base.Jump(context);
        if (context.performed && !IsGrounded && _canDoubleJump && !IsWallSliding)
        {
            GameManager.Manager.HasDoubleJumped = true;
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            _canDoubleJump = false;
        }
    }

    private void StartWalkJump()
    {
        Debug.Log("Start Walking");
        startedWalking = true;

        var boost = IsGrounded ? startWalkRotationBoost : 1;
        _rb.AddTorque(rotationSpeed * Time.fixedDeltaTime * Mathf.Sign(_movementInput.x) * -1 * boost);
    }

    private void UpdateRotationSpeed()
    {
        if (Mathf.Abs(_rb.velocity.y) < 3f)
        {
            rotationSpeed = walkingRotationSpeed;
            return;
        }

        rotationSpeed = walkingRotationSpeed / airFactor;
    }

    public void SwitchGravity(InputAction.CallbackContext context)
    {
        _rb.gravityScale *= -1;
    }
    
}
