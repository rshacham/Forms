using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomPoints : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    
    [SerializeField] private float zoom;
    [SerializeField] private float offsetX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //_camera.CameraOffsets.x = offsetX;
    }
}
