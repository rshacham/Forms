using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;
    
    [SerializeField] private Button redButton;
    [SerializeField] private Button yellowButton;

    private Color _color; 


    private void Awake()
    {
        if (gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(this);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        redButton.onClick.AddListener(ChangeSceneRed);
        yellowButton.onClick.AddListener(ChangeSceneYellow);
    }

    private void ChangeSceneRed()
    {
        _color = Color.red;
        SceneManager.LoadScene("ChangedColors");
    }
    
    private void ChangeSceneYellow()
    {
        _color = Color.yellow;
        SceneManager.LoadScene("ChangedColors");
    }

    public Color getColor()
    {
        return _color;
    }
    
    
}
