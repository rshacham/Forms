using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    [SerializeField] private GameObject end;

    private Animator[] _animators;
    
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayersManager.Manager.ActivePlayerScript.ResetMovement();
        PlayersManager.Manager.ActivePlayerScript.RemoveMoveables();
        PlayersManager.Manager.ActivePlayerScript.CanMove = false;
        
        if (_animators != null)
        {
            foreach (var animator in _animators)
                animator.SetBool("got", true);    
        }
        
        end.SetActive(true);
    }
    
    }
