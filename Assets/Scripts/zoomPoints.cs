using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class zoomPoints : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private bool active;
    [SerializeField] private float zoom;
    [SerializeField] private Vector2 offset;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players") && active)
        {
            camera.Zoom = zoom;
            camera.CameraOffsets = offset;
        }
        
    }
}
