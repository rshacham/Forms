using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Transform _cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        _cameraTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _cameraTransform.position = new Vector3(
            playerTransform.position.x,
            _cameraTransform.position.y,
            _cameraTransform.position.z);
    }
}
