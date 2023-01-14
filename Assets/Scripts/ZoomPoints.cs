using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ZoomPoints : MonoBehaviour
{
    private Camera camera;

    [SerializeField] private float newZoom;
    [SerializeField] private Vector2 newOffset;

    [SerializeField] private bool freezeHorizontal;
    [SerializeField] private bool freezeVertical;
    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players"))
        {
            if (newZoom > 1)
            {
                camera.Zoom = newZoom;
            }

            if (newOffset.magnitude > 1f)
            {
                camera.TargetOffsets = newOffset;
            }
        }

        camera.FreezeHorizontal = freezeHorizontal;
        camera.FreezeVertical = freezeVertical;
    }
}
