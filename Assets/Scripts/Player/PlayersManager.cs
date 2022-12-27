using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager Manager;
    
    [SerializeField] private GameObject[] players;
    private GameObject _activePlayer;

    public Player ActivePlayerScript { get; set; }
    
    private Transform _activePlayerTransform;
    private Vector2 _previousVelocity;

    private int _currentPlayer;

    #region PlayerHolder
    private Transform _playersParent;
    public Transform PlayersParent => _playersParent;
    #endregion
    public bool HasChangedShape { get; set; } = false;
    public bool HasJumped { get; set; } = false;
    public Vector2 PlatformFactor { get; set; }

    private void Awake()
    {
        Manager = this;
    }

    private void Start()
    {
        _activePlayer = players[_currentPlayer];
        _activePlayerTransform = _activePlayer.GetComponent<Transform>();
        _playersParent = _activePlayerTransform.parent;
        UpdatePlayerScript();
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
    }
    
    private void AfterChoosingActivePlayer()
    {
        _activePlayer.transform.position = _activePlayerTransform.position;
        _activePlayerTransform = _activePlayer.transform;
        UpdatePlayerScript();
        _activePlayer.SetActive(true);
        ActivePlayerScript.PlayerRigidBody.velocity = _previousVelocity;
    }

    public void ChangeToCircle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BeforeChoosingActivePlayer(0);
            AfterChoosingActivePlayer();
        }
    }
    
    public void ChangeToSquare(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BeforeChoosingActivePlayer(1);
            AfterChoosingActivePlayer();
        }
    }
    
    public void ChangeToTriangle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            BeforeChoosingActivePlayer(0);
            AfterChoosingActivePlayer();
        }
    }

    public void UpdatePlayerScript()
    {
        switch (_currentPlayer)
        {
            case 0:
                ActivePlayerScript = _activePlayer.GetComponent<TrianglePlayer>();
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
        _activePlayer.transform.localPosition = GameManager.Manager.ReturnPoint;
        ActivePlayerScript.ResetMovement();
    }
    
    

}
