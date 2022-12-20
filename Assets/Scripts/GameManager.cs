using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;
    
    #region Layers
    [SerializeField] private LayerMask wallLayer;
    public LayerMask WallLayer => wallLayer;
    
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;
    #endregion

    private Vector2 _returnPoint;

    public Vector2 ReturnPoint
    {
        get => _returnPoint;
        set => _returnPoint = value;
    }

    void Awake()
    {
        Manager = this;
    }


    public void Losing()
    {
        // get the player to the returnPoint coordinates from the Player script
    }
    
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
