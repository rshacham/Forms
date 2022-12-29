using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrianglePlayer : Player
{
    private bool _canDoubleJump = true;

    #region Wall Sliding
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private Transform[] wallCheck;
    private bool _isFacingRight = true;
    #endregion

    public new void Update()
    {
        base.Update();
        if (IsGrounded)
        {
            _canDoubleJump = true;
        }
    }

    public new void FixedUpdate()
    {
        if (!IsWallSliding)
        {
            base.FixedUpdate();
        }
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
    
    
}
