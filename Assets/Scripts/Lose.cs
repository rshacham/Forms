using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lose : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("players"))
        {
            // call the PlayersManager method to change the position
            PlayersManager.playersManager.HandleLose();
            // gameManager.Reset();
        }
    }
}
