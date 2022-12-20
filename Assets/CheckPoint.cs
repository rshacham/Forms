using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Vector3 checkPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players"))
        {
            GameManager.Manager.ReturnPoint = checkPoint;
        }
    }
}
