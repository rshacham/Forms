using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class zoomPoints : MonoBehaviour
{
    private UnityEngine.Camera camera;
    
    [SerializeField] private float zoom;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        camera = UnityEngine.Camera.main;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players"))
        {
            camera.orthographicSize = zoom;
            camera.GetComponent<Camera>().CameraOffsets = offset;
        }
        
    }
}
