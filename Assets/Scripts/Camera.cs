using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bottomFollowPlayerHeight;
    [SerializeField] private float upperFollowPlayerHeight;
    
    [SerializeField] private Vector3 cameraOffsets;

    public Vector3 CameraOffsets
    {
        get => cameraOffsets;
        set => cameraOffsets = value;
    }


    [SerializeField] [Range(0.01f, 1f)]
    private float smoothSpeed;
    
    private Vector2 _velocity = Vector2.zero;

    public bool FollowPlayerVertical { get; set; } = false;

    private Transform _cameraTransform;
    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        FollowPlayerVertical = true;
    }

    void LateUpdate()
    {
        Vector2 desiredPosition = playerTransform.position + cameraOffsets;
        var cameraPosition = _cameraTransform.position;
        var playerPosition = playerTransform.position;

        // if ((playerPosition.y > bottomFollowPlayerHeight && playerPosition.y < upperFollowPlayerHeight)
        //     || FollowPlayerVertical)
        // {
        //     desiredPosition.y = playerPosition.y;
        // }
        
        var newPosition = Vector2.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);


        if (playerTransform)
        {
            transform.position = new Vector3(newPosition.x, newPosition.y, cameraPosition.z);
        }
    }
}
