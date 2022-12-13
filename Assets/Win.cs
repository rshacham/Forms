using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        EndGame();
    }

    private void EndGame()
    {
        var player = FindObjectOfType<Player>();
        player.CanMove = false;
    }
}
