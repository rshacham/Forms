using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TrianglePlayer : Player
{
    private bool _canDoubleJump = true;

    public new void Update()
    {
        base.Update();
        if (_isGrounded)
        {
            _canDoubleJump = true;
        }
    }
    
    public override void Jump(InputAction.CallbackContext context)
    {
        base.Jump(context);
        if (context.performed && !_isGrounded && _canDoubleJump)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, jumpingPower);
            _canDoubleJump = false;
        }
    }
}
