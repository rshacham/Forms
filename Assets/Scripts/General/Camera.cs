using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private bool editMode;
    
    private UnityEngine.Camera _camera;
    
    [SerializeField] private Transform playerTransform;

    public Vector3 PreviousOffsets { get; set; }
    
    public Vector3 TargetOffsets { get; set; }

    [SerializeField] private Vector3 cameraOffsets;

    [SerializeField] private bool staticCamera;

    public bool FreezeHorizontal { get; set; }
    public bool FreezeVertical { get; set; }
    
    public Vector3 CameraOffsets
    {
        get => cameraOffsets;
        set => cameraOffsets = value;
    }

    public float Zoom { get; set; }

    [SerializeField] [Range(0.01f, 1f)]
    private float followSmoothSpeed;
    
    [SerializeField] [Range(0.01f, 1f)]
    private float zoomSmoothSpeed;

    [SerializeField] [Range(0.01f, 1f)] 
    private float offsetSmoothSpeed;

    private Vector2 _velocity = Vector2.zero;
    private float _otherVelocity = 0f;
    public bool FollowPlayerVertical { get; set; } = false;

    private Transform _cameraTransform;
    void Start()
    {
        _camera = GetComponent<UnityEngine.Camera>();
        _cameraTransform = GetComponent<Transform>();
        FollowPlayerVertical = true;
        Zoom = _camera.orthographicSize;
        TargetOffsets = cameraOffsets;

        if (staticCamera)
        {
            FreezeHorizontal = true;
            FreezeVertical = true;
        }
    }

    void LateUpdate()
    {
        FollowPlayer();

        if (!editMode)
        {
            SmothZoom();

            SmoothOffset();
        }
    }

    private void SmoothOffset()
    {
        if ((cameraOffsets - TargetOffsets).magnitude > 0.1f)
        {
            var newOffset = Vector2.SmoothDamp(
                cameraOffsets, TargetOffsets, ref _velocity, offsetSmoothSpeed);

            cameraOffsets = newOffset;

            if ((TargetOffsets - cameraOffsets).magnitude <= 0.1f || editMode)
            {
                TargetOffsets = cameraOffsets;
            }
        }
    }

    private void SmothZoom()
    {
        var newZoom = Mathf.SmoothDamp(_camera.orthographicSize, Zoom, ref _otherVelocity, zoomSmoothSpeed);

        _camera.orthographicSize = newZoom;
    }

    private void FollowPlayer()
    {
        if (FreezeHorizontal && FreezeVertical)
        {
            return;
        }
        
        var playerPosition = playerTransform.position;
        
        var horizontal = transform.position.x;
        var vertical = transform.position.y;
        var depth = transform.position.z;

        Vector3 desiredPosition = playerPosition;

        var offsetFix = desiredPosition + cameraOffsets;
        
        if (FreezeHorizontal)
        {
            offsetFix.x = horizontal;
        }

        if (FreezeVertical)
        {
            offsetFix.y = vertical;
        }

        var newPosition = Vector2.SmoothDamp(transform.position, offsetFix, ref _velocity, followSmoothSpeed);

        if (playerTransform)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, depth);
        }
    }
}
