using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    public bool HasDoubleJumped { get; set; } = false;

    public bool HasChangedToSquare { get; set; } = false;

    public bool HasChangedToTriangle { get; set; } = false;
    
    public bool HasChangedToCircle { get; set; } = false;

    #region Layers
    [SerializeField] private LayerMask wallLayer;
    public LayerMask WallLayer => wallLayer;
    
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;
    #endregion

    public Vector3 ReturnPoint { get; set; } = new (11.5f, -8.42f, 0.282f);
    public bool HasWallJumped { get; set; }

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
