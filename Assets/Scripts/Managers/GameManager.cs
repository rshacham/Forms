using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Manager;

    [SerializeField] private GameObject[] achievement;

    public bool HasDoubleJumped { get; set; }

    public bool HasChangedToSquare { get; set; }

    public bool HasChangedToTriangle { get; set; }
    
    public bool HasChangedToCircle { get; set; }

    #region Layers
    [SerializeField] private LayerMask wallLayer;
    public LayerMask WallLayer => wallLayer;
    
    [SerializeField] private LayerMask groundLayer;
    public LayerMask GroundLayer => groundLayer;
    #endregion

    public Vector3 ReturnPoint { get; set; } = new (11.5f, -8.42f, 0.282f);
    public bool HasWallJumped { get; set; }

    private void Awake()
    {
        Manager = this;
    }

    private void Start()
    {
        // check if works
        ReturnPoint = FindObjectOfType<Player>().transform.localPosition;
    }

    public void GetAchievement(int shape)
    {
        achievement[shape].SetActive(true);
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
