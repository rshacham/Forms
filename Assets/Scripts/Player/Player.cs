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

    #region Basic Movement
    [SerializeField] private float maxSpeed;
    [SerializeField] protected float acceleration;
    #endregion
    
    #region Jumping
    [SerializeField] private float afterJumpFallSpeed;
    [SerializeField] protected float jumpingPower;
    protected bool IsGrounded = false;
    #endregion
    
    #region Default Player Settings
    [SerializeField] protected DefaultPlayerSettings defaultSettings;
    [SerializeField] protected bool useDefaultGravity;
    [SerializeField] protected bool useDefaultJumpingPower;
    [SerializeField] protected bool useDefaultAcceleration;
    #endregion
    
    private bool _canMove = true;
    public bool CanMove {
        get { return _canMove; }
        set { _canMove = value; }
    }
    
    private Collider2D collider;
    private float _speed_t = 0;
    protected Rigidbody2D _playerRigidBody;
    public bool OnPlatform { get; set; }

    
    public Rigidbody2D PlayerRigidBody
    {
        get => _playerRigidBody;
        set => _playerRigidBody = value;
    }
    
    private Vector2 _desiredVelocity;
    
    #region Wall Sliding
    protected bool IsWallSliding = false;
    #endregion

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
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
            _playerRigidBody.gravityScale = defaultSettings.gravity;
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
    }

    protected void FixedUpdate()
    {
        var platformFactor = Vector2.zero;
        
        
        if (_canMove)
        {
            var playerSpeed = Input.GetAxis("Horizontal") * acceleration;
            _playerRigidBody.velocity = new Vector2(playerSpeed, _playerRigidBody.velocity.y);
        }
    }
    public virtual void Move(InputAction.CallbackContext context)
    {
        
    }

    public virtual void Jump(InputAction.CallbackContext context)
    {
        
        
        var currentVelocity = _playerRigidBody.velocity;
        if (context.performed && IsGrounded)
        {
            _playerRigidBody.velocity = new Vector2(currentVelocity.x, jumpingPower);
        }

        if (context.canceled && currentVelocity.y > 0)
        {
            _playerRigidBody.velocity = new Vector2(currentVelocity.x, currentVelocity.y / afterJumpFallSpeed);
        }
    }
    
    private bool IsWalled(IEnumerable<Transform> wallCheck)
    {
        foreach (var checker in wallCheck)
        {
            if (Physics2D.OverlapCircle(checker.position, 0.2f, wallLayer))
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
            var velocity = _playerRigidBody.velocity;
            _playerRigidBody.velocity = new Vector2(velocity.x,
                Mathf.Clamp(velocity.y, -wallSlidingSpeed, float.MaxValue));
            IsWallSliding = true;
        }

        else
        {
            IsWallSliding = false;
        }
    }

    private bool CheckIfGrounded()
    {
        var colliderBounds = collider.bounds;
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(colliderBounds.center,
            colliderBounds.size, 0,
            Vector2.down, 1f, groundLayer);
        // if not touching the ground, return false
        return raycastHit2D.collider != null;
    }

    public void ResetMovement()
    {
        _playerRigidBody.velocity = Vector2.zero;
    }

    public void CanRotate(bool canRotate)
    {
        if (canRotate)
        {
            PlayerRigidBody.constraints = RigidbodyConstraints2D.None;
            return;
        }
        
        _playerRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    
}
