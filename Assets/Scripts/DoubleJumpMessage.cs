using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleJumpMessage : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject doubleJumpMessage;
    [SerializeField] private GameObject changeToSquareMessage;
    [SerializeField] private GameObject afterSquareChangeMessage;
    [SerializeField] private GameObject changeToTriangleMessage;

    [SerializeField] private string afterSquareChangeText;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Manager.HasDoubleJumped)
        {
            if (doubleJumpMessage != null)
            {
                doubleJumpMessage.SetActive(true);
            }
        }

        if (!GameManager.Manager.HasChangedToSquare)
        {
            if (changeToSquareMessage != null)
            {
                changeToSquareMessage.SetActive(true);
            }
        }

        if (!GameManager.Manager.HasChangedToTriangle)
        {
            if (changeToTriangleMessage != null)
            {
                changeToTriangleMessage.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (GameManager.Manager.HasDoubleJumped && doubleJumpMessage != null)
        {
            doubleJumpMessage.SetActive(false);
        }
        if (GameManager.Manager.HasChangedToSquare && changeToSquareMessage != null && afterSquareChangeMessage != null)
        {
            changeToSquareMessage.SetActive(false);
            afterSquareChangeMessage.SetActive(true);
        }

        if (GameManager.Manager.HasWallJumped && afterSquareChangeMessage != null)
        {
            afterSquareChangeMessage.SetActive(false);
        }

        if (GameManager.Manager.HasChangedToTriangle && changeToTriangleMessage != null)
        {
            changeToTriangleMessage.SetActive(false);
        }
    }
}
