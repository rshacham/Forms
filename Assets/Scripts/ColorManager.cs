using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorManager : MonoBehaviour
{

    public static ColorManager colorManager;
    
    [SerializeField] private Button redButton;
    [SerializeField] private Button yellowButton;

    private Color _color; 


    private void Awake()
    {
        if (colorManager == null)
        {
            colorManager = this;
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
