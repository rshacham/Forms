using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private TextMeshProUGUI winning;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("players"))
        {
            winning.gameObject.SetActive(true);
            var player = FindObjectOfType<Player>();
            player.CanMove = false;
        }
    }

    public void EndGame()
    {
        gameManager.Reset();
        
        // var player = FindObjectOfType<Player>();
        // player.CanMove = false;
    }
}
