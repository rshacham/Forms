using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float afterJumpFallSpeed;
    [SerializeField] private float jumpingPower;
    private bool _isGrounded = false;
    private bool _isJumping = false;
    
    
    private bool _canMove = true;
    private BoxCollider2D _boxCollider2D;
    private float _speed_t = 0;
    private Rigidbody2D _playerRigidBody;
    private Vector2 _desiredVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _isGrounded = CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        var playerSpeed = Input.GetAxis("Horizontal") * acceleration;
        _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
        // _playerRigidBody.velocity = _desiredVelocity;
    }

    public void Move(InputAction.CallbackContext context)
    {
        // float movementDirection = context.ReadValue<Vector2>().x;
        // Vector2 currentVelocity = _playerRigidBody.velocity;
        // if (_canMove)
        // {
        //     var xVelocity = Mathf.Min(currentVelocity.x + acceleration * movementDirection, maxSpeed);
        //     _desiredVelocity = new Vector2(xVelocity, currentVelocity.y);
        // }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        var currentVelocity = _playerRigidBody.velocity;
        if (context.performed && _isGrounded)
        {
            _playerRigidBody.velocity = new Vector2(currentVelocity.x, jumpingPower);
        }

        if (context.canceled && currentVelocity.y > 0)
        {
            _playerRigidBody.velocity = new Vector2(currentVelocity.x, currentVelocity.y / afterJumpFallSpeed);
        }
    }

    private bool CheckIfGrounded()
    {
        var colliderBounds = _boxCollider2D.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(colliderBounds.center,
            colliderBounds.size, 0,
            Vector2.down, 0.1f, groundLayer);
        // if not touching the ground, return false
        return raycastHit2D.collider != null;
    }
}
