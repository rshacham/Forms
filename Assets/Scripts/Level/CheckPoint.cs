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

    [SerializeField] private AudioClip checkPointSound;

    private void Start()
    {
        if (checkPoint.magnitude < 0.1)
        {
            checkPoint = transform.position;
        }
        
        camera = FindObjectOfType<Camera>();
        checkPoint = checkPoint.magnitude < 1 ? transform.position : checkPoint;

        _animator = gameObject.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players") && !_reachecCheckpoint && checkPointActive)
        {
            if (_animator != null)
            {
                _animator.SetBool("got", true);    
            }
            
            _reachecCheckpoint = true;
            GameManager.Manager.ReturnPoint = checkPoint;
            SoundManager.Manager.PlaySound(checkPointSound);
        }
        
    }
}
