using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
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

    private Fade fade;
    
    public bool IsGameOver { get; set; }
    private bool buttonPressed;
    private bool isJoystick;

    public Fade Fade
    {
        get => fade;
        set => fade = value;
    }

    private void Awake()
    {
        Manager = this;
        int joystickLen = Input.GetJoystickNames().Length;
        isJoystick = gameObject.AddComponent<JoystickUtils>().IsConnected(joystickLen);
    }

    private void Start()
    {
        // check if works
        ReturnPoint = FindObjectOfType<Player>().transform.localPosition;
        Fade = GetComponentInChildren<Fade>();

        if (Fade.FadeOutAtStart)
        {
            Fade.StartCoroutine(Fade.StartFade(false));
        }
    }

    private void Update()
    {
        if (IsGameOver && isJoystick)
        {
            var gamepadButtonPressed = Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic);
            buttonPressed = gamepadButtonPressed;
            if (buttonPressed)
            {
                Reset();
            }
        }
    }
    
    public void ButtonPressed()
    {
        if (IsGameOver)
        {
            buttonPressed = true;
            Reset();
        }
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
        SceneManager.LoadScene(0);
    }

    public void LoadNewScene(string sceneString, int sceneNum)
    {
        if (sceneString != "")
        {
            SceneManager.LoadScene(sceneString);
            return;
        }

        SceneManager.LoadScene(sceneNum);
    }

}
