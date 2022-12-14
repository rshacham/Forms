using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("players"))
        {
            gameManager.Reset();
        }
    }
}
