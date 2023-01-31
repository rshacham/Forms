using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager Manager;

    public bool CanChangeShape { get; set; } = true;

    private ColorsManager _colorsManager;
    
    [SerializeField] private GameObject[] players;
    private GameObject _activePlayer;

    public Player ActivePlayerScript { get; set; }
    
    private Transform _activePlayerTransform;
    
    private Animator _activeAnimator;
    private string previousPlayerName;
    private string currentPlayerName;
    
    private Vector2 _previousVelocity;

    private int _currentPlayer;

    [SerializeField] private bool canSquare;

    public bool HasChangedShape { get; set; } = false;
    public bool HasJumped { get; set; } = false;
    public bool CanSquare { get => canSquare; set => canSquare = value;}
    
    [SerializeField] private GameObject startingPoint;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] switchPlayerSounds;
    [SerializeField] private AudioClip[] deathSounds;

    private void Awake()
    {
        Manager = this;
    }

    private void Start()
    {
        _activePlayer = players[_currentPlayer];
        _activePlayerTransform = _activePlayer.GetComponent<Transform>();
        _activeAnimator = _activePlayer.GetComponent<Animator>();
        if (startingPoint != null)
        {
            _activePlayer.transform.position = startingPoint.transform.position;
        }

        currentPlayerName = "Circle";

        UpdatePlayerScript();
        if (startingPoint != null)
        {
            CanSquare = true;
        }
        AfterChoosingActivePlayer();

        _colorsManager = GetComponentInChildren<ColorsManager>();
        _colorsManager.ActivePlayer = _activePlayer;
        _colorsManager.ChangeCircleColor();
    }
    
    private void Update()
    {
        transform.position = _activePlayerTransform.position;
    }

    public void SwitchPlayer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HasChangedShape = true;
            _activePlayer.SetActive(false);
            _activePlayer = players[++_currentPlayer % 3];
            _activePlayer.transform.position = _activePlayerTransform.position;
            _activePlayerTransform = _activePlayer.transform;
            UpdatePlayerScript();
            _activePlayer.SetActive(true);
        }
    }

    private void BeforeChoosingActivePlayer(int newPlayer)
    {
        _previousVelocity = ActivePlayerScript.PlayerRigidBody.velocity;
        _currentPlayer = newPlayer;
        HasChangedShape = true;
        _activePlayer.SetActive(false);
        _activePlayer = players[newPlayer];
        SoundManager.Manager.PlayRandomSound(switchPlayerSounds);
        _colorsManager.ActivePlayer = _activePlayer;
    }
    
    private void AfterChoosingActivePlayer()
    {
        _activePlayer.transform.position = _activePlayerTransform.position;
        _activePlayerTransform = _activePlayer.transform;
        _activeAnimator = _activePlayer.GetComponent<Animator>();
        UpdatePlayerScript();
        _activePlayer.SetActive(true);
        _activeAnimator.SetTrigger(previousPlayerName);
        ActivePlayerScript.PlayerRigidBody.velocity = _previousVelocity;
        if (UIManager.Manager != null)
        {
            UIManager.Manager.ChangeActivePlayerUI(currentPlayerName);    
        }
        
    }

    public void ChangeToCircle()
    {
        BeforeChoosingActivePlayer(0);
        currentPlayerName = "Circle";
        AfterChoosingActivePlayer();
        _colorsManager.ChangeCircleColor();
    }
    
    public void ChangeToSquare()
    {
        BeforeChoosingActivePlayer(1);
        currentPlayerName = "Square";
        AfterChoosingActivePlayer();
        _colorsManager.ChangeSquareColor();
    }
    
    public void ChangeToTriangle()
    {
        BeforeChoosingActivePlayer(2);
        currentPlayerName = "Triangle";
        AfterChoosingActivePlayer();
        _colorsManager.ChangeTriangleColor();
    }

    public void UpdatePlayerScript()
    {
        switch (_currentPlayer)
        {
            case 0:
                ActivePlayerScript = _activePlayer.GetComponent<CirclePlayer>();
                break;
            case 1:
                ActivePlayerScript = _activePlayer.GetComponent<SquarePlayer>();
                break;
            case 2:
                ActivePlayerScript = _activePlayer.GetComponent<TrianglePlayer>();
                break;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        ActivePlayerScript.Move(context);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        HasJumped = true;
        ActivePlayerScript.Jump(context);
    }
    
    // method to change position of the player (according to the coordinates in gameManger) when lose
    // calls the resetMovement (method in Player script)
    public void HandleLose()
    {
        // SoundManager.Manager.PlayRandomSound(deathSounds);
        _activePlayer.transform.position = GameManager.Manager.ReturnPoint;
        ActivePlayerScript.ResetMovement();
    }
    
    public void ToCircle(InputAction.CallbackContext context)
    {
        if (!CanChangeShape)
        {
            return;
        }

        if (context.performed)
        {
            if (!CanChangeShape)
            {
                return;
            }
            
            previousPlayerName = currentPlayerName;
            ChangeToCircle();
            GameManager.Manager.HasChangedToCircle = true;
        }
    }
    
    public void ToSquare(InputAction.CallbackContext context)
    {
        if (context.performed && CanSquare)
        {
            if (!CanChangeShape)
            {
                return;
            }
            
            previousPlayerName = currentPlayerName;
            ChangeToSquare();
            GameManager.Manager.HasChangedToSquare = true;
        }
    }

    public void ToTriangle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!CanChangeShape)
            {
                return;
            }
            
            previousPlayerName = currentPlayerName;
            ChangeToTriangle();
            GameManager.Manager.HasChangedToTriangle = true;
        }
    }
}
