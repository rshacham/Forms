using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private UnityEngine.Camera _camera;
    
    [SerializeField] private Transform playerTransform;

    public Vector3 PreviousOffsets { get; set; }

    [SerializeField] private Vector3 cameraOffsets;
    
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
        PreviousOffsets = cameraOffsets;
    }

    void LateUpdate()
    {
        Vector2 desiredPosition = playerTransform.position + cameraOffsets;
        var cameraPosition = _cameraTransform.position;

        // if ((playerPosition.y > bottomFollowPlayerHeight && playerPosition.y < upperFollowPlayerHeight)
        //     || FollowPlayerVertical)
        // {
        //     desiredPosition.y = playerPosition.y;
        // }

        
        var newPosition = Vector2.SmoothDamp(transform.position, desiredPosition, ref _velocity, followSmoothSpeed);
        
        if (playerTransform)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, cameraPosition.z);
        }
        
        var newZoom = Mathf.SmoothDamp(_camera.orthographicSize, Zoom, ref _otherVelocity, zoomSmoothSpeed);
        
        _camera.orthographicSize = newZoom;

        if ((PreviousOffsets - cameraOffsets).magnitude > 0.4f)
        {
            var newOffset = Vector2.SmoothDamp(
                PreviousOffsets,cameraOffsets, ref _velocity, offsetSmoothSpeed);
        
            cameraOffsets = newOffset;

            if ((PreviousOffsets - cameraOffsets).magnitude <= 0.4f)
            {
                PreviousOffsets = cameraOffsets;
            }
        }

        

    }
}
