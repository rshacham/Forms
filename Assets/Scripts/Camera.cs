using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float bottomFollowPlayerHeight;
    [SerializeField] private float upperFollowPlayerHeight;
    
    [SerializeField] private Vector2 cameraOffsets;

    public bool FollowPlayerVertical { get; set; } = false;

    private Transform _cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
        FollowPlayerVertical = true;
    }

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = _cameraTransform.position;
        var playerPosition = playerTransform.position;
        if ((playerPosition.y > bottomFollowPlayerHeight && playerPosition.y < upperFollowPlayerHeight)
            || FollowPlayerVertical)
        {
            cameraPosition.y = playerPosition.y;
        }
        if (playerTransform) 
            _cameraTransform.position = new Vector3(
            playerTransform.position.x + cameraOffsets.x,
            cameraPosition.y + cameraOffsets.y,
            cameraPosition.z);
    }
}
