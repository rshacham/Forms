using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelManager : MonoBehaviour
{
    public bool ShouldSquareUI { get; set; }
    public bool ShouldTriangleUI { get; set; }
    
    void Start()
    {
        UIManager.Manager.MakeTransparentUI("Triangle", 1);
        UIManager.Manager.MakeTransparentUI("Square", 1);
    }

    void Update()
    {
        ShouldSquareUI = GameManager.Manager.HasChangedToSquare;
        ShouldTriangleUI = GameManager.Manager.HasChangedToTriangle;
    }
}
