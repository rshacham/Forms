using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleJumpMessage : MonoBehaviour
{
    [SerializeField] private GameObject doubleJumpMessage;
    [SerializeField] private GameObject changeToSquareMessage;
    [SerializeField] private GameObject changeToTriangleMessage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.HasDoubleJumped)
        {
            doubleJumpMessage.SetActive(true);
        }

        if (!GameManager.HasChangedToSquare)
        {
            changeToSquareMessage.SetActive(true);
        }
    }

    private void Update()
    {
        if (GameManager.HasDoubleJumped)
        {
            doubleJumpMessage.SetActive(false);
        }
        if (GameManager.HasChangedToSquare)
        {
            changeToSquareMessage.SetActive(false);
            changeToTriangleMessage.SetActive(true);
        }
        if (GameManager.HasChangedToTriangle)
        {
            changeToTriangleMessage.SetActive(false);
        }
    }
}
