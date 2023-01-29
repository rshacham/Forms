using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private bool checkPointActive = true;
    [SerializeField] private Vector3 checkPoint;
    private bool _reachecCheckpoint = false;
    
    private Camera camera;

    private Animator _animator;

    private bool followVertical;

    private bool followHorizontal;
    
    [Header("Camera Transition")]
    [SerializeField] private float zoom;
    [SerializeField] private Vector2 offset;


    private void Start()
    {
        camera = FindObjectOfType<Camera>();
        checkPoint = checkPoint.magnitude < 1 ? transform.position : checkPoint;

        _animator = gameObject.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players") && !_reachecCheckpoint && checkPointActive)
        {
            _animator.SetBool("got", true);
            _reachecCheckpoint = true;
            GameManager.Manager.ReturnPoint = checkPoint;
        }
    }
}
