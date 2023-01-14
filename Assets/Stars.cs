using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] private int shape;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("players"))
        {
            GameManager.Manager.GetAchievement(shape);
            gameObject.SetActive(false);
        }
    }
}
