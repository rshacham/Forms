using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowMessages : MonoBehaviour
{
    [SerializeField] private PlayersManager playersManager;
    [SerializeField] private TextMeshProUGUI jumpMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playersManager.HasJumped)
        {
            jumpMessage.gameObject.SetActive(true);
        }
        if (playersManager.HasJumped)
        {
            jumpMessage.gameObject.SetActive(false);
        }
        
    }
}
