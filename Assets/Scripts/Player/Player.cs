using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public abstract class Player : MonoBehaviour
{
    protected LayerMask groundLayer;
    protected LayerMask wallLayer;
    
    # region Basic Components
    protected Rigidbody2D _rb;
    protected float _gravityScale;
    # endregion

    [Header("Basic Movement")]
    #region Basic Movement
    [SerializeField] private float movementSpeed;
    [SerializeField] protected float acceleration;
    [SerializeField] private float decceleration;
    [SerializeField] private float velPower;

    protected bool _canMove = true;
    public bool CanMove {
        get { return _canMove; }
        set { _canMove = value; }
    }

    protected Vector2 _movementInput;
    private Vector2 _smoothMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    #endregion
    
    [Header("Jumping")]
    #region Jumping
    [SerializeField] private float afterJumpFallSpeed;
    [SerializeField] protected float jumpingPower;
    [SerializeField] private float jumpCoyoteTime;
    [SerializeField] private float jumpBufferTime;
    [SerializeField] private float jumpCutMultiplier;
    [SerializeField] protected float fallGravityMultiplier;
    
    [SerializeField] private float isGroundedRayLength;
    protected bool IsGrounded = false;
    private bool _isJumping = false;
    private float _lastGroundedTime;
    private float _lastJumpTime = 0;
    private bool _jumpInputReleased;
    #endregion
    
    #region Default Player Settings
    [SerializeField] protected DefaultPlayerSettings defaultSettings;
    [SerializeField] protected bool useDefaultGravity;
    [SerializeField] protected bool useDefaultJumpingPower;
    [SerializeField] protected bool useDefaultAcceleration;
    #endregion
    
    private Collider2D collider;
    private float _speed_t = 0;

    public bool OnPlatform { get; set; }
    
    public Rigidbody2D PlayerRigidBody
    {
        get => _rb;
        set => _rb = value;
    }
    
    private Vector2 _desiredVelocity;
    
    #region Wall Sliding
    protected bool IsWallSliding = false;
    #endregion

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _gravityScale = _rb.gravityScale;
        collider = GetComponent<Collider2D>();
    }

    protected void Start()
    {
        groundLayer = GameManager.Manager.GroundLayer;
        wallLayer = GameManager.Manager.WallLayer;
        InitializeDefaultSettings();
    }

    private void InitializeDefaultSettings()
    {
        if (useDefaultGravity)
        {
            _rb.gravityScale = defaultSettings.gravity;
        }

        if (useDefaultAcceleration)
        {
            acceleration = defaultSettings.acceleration;
        }

        if (useDefaultJumpingPower)
        {
            jumpingPower = defaultSettings.jumpingPower;
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        IsGrounded = CheckIfGrounded();
        _lastGroundedTime -= Time.deltaTime;
        _lastJumpTime -= Time.deltaTime;
    }

    protected void FixedUpdate()
    {
        if (_canMove)
        {
            BasicMovement();
        }
        
        UpdateFallGravity();

        
        
        // if (_canMove)
        // {
        //     _smoothMovementInput = Vector2.SmoothDamp(
        //         _smoothMovementInput,
        //         _movementInput,
        //         ref _movementInputSmoothVelocity,
        //         smoothSpeed);
        //     //
        //     _rb.velocity = new Vector2(_smoothMovementInput.x * acceleration, _rb.velocity.y);
        // }
        
        // if (_canMove)
        // {
        //     var playerSpeed = Input.GetAxis("Horizontal") * acceleration;
        //     _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
        // }
    }

    protected void UpdateFallGravity()
    {
        if (_rb.velocity.y < 0.01f && !IsGrounded)
        {
            _rb.gravityScale = _gravityScale * fallGravityMultiplier;
        }

        else
        {
            _rb.gravityScale = _gravityScale;
        }
    }

    protected void BasicMovement()
    {
        _movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float targetSpeed = _movementInput.x * movementSpeed;
        float speedDif = targetSpeed - _rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);
        
        _rb.AddForce(movement * Vector2.right);
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
        
    }

    public virtual void Jump(InputAction.CallbackContext context)
    {
        var currentVelocity = _rb.velocity;
        // if (context.performed && IsGrounded)
        // {
        //     _rb.velocity = new Vector2(currentVelocity.x, jumpingPower);
        // }
        //
        // if (context.canceled && currentVelocity.y > 0)
        // {
        //     _rb.velocity = new Vector2(currentVelocity.x, currentVelocity.y / afterJumpFallSpeed);
        // }
        _lastJumpTime = jumpBufferTime;
        
        if (context.performed && IsGrounded && _lastGroundedTime > 0 && _lastJumpTime > 0)
        {
            _rb.AddForce(Vector2.up * jumpingPower, ForceMode2D.Impulse);
            _lastGroundedTime = 0;
            _lastJumpTime = 0;
            _isJumping = true;
            _jumpInputReleased = false;
        }

        if (context.canceled)
        {
            if (_rb.velocity.y > 0 && _isJumping)
            {
                _rb.AddForce(Vector2.down * _rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);    
            }

            _jumpInputReleased = true;
            _lastJumpTime = 0;
        }

    }

    protected bool CheckIfGrounded()
    {
        var colliderBounds = collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(colliderBounds.center,
            colliderBounds.size, 0,
            Vector2.down, isGroundedRayLength, groundLayer);
        // if not touching the ground, return false
        bool isGrounded = (raycastHit2D.collider != null);
        if (isGrounded)
        {
            _lastGroundedTime = jumpCoyoteTime;
        }

        return isGrounded;
    }

    public void ResetMovement()
    {
        _rb.velocity = Vector2.zero;
    }

    public void CanRotate(bool canRotate)
    {
        if (canRotate)
        {
            PlayerRigidBody.constraints = RigidbodyConstraints2D.None;
            return;
        }
        
        _rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

}
