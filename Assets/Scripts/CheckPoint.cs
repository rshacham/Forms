using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector3 checkPoint;
    private bool _reachecCheckpoint = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players") && !_reachecCheckpoint)
        {
            _reachecCheckpoint = true;
            GameManager.Manager.ReturnPoint = checkPoint;
        }
    }
}
