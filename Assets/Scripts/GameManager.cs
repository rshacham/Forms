using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    private static bool hasDoubleJumped = false;

    private static bool hasChangedToSquare = false;
    
    private static bool hasChangedToTriangle = false;
    
    public static bool HasDoubleJumped
    {
        get => hasDoubleJumped;
        set => hasDoubleJumped = value;
    }
    
    public static bool HasChangedToSquare
    {
        get => hasChangedToSquare;
        set => hasChangedToSquare = value;
    }

    public static bool HasChangedToTriangle
    {
        get => hasChangedToTriangle;
        set => hasChangedToTriangle = value;
    }


    #region Layers
    [SerializeField] private LayerMask wallLayer;
    public LayerMask WallLayer => wallLayer;
    
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;
    #endregion

    public Vector3 ReturnPoint { get; set; } = new (11.5f, -8.42f, 0.282f);

    void Awake()
    {
        Manager = this;
    }

    private void Start()
    {
        // check if works
        ReturnPoint = FindObjectOfType<Player>().transform.localPosition;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
