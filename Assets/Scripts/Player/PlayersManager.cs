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

    [Header("Colors of not moving platforms")]
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private Color platformColorInCircle;
    [SerializeField] private Color platformColorInSquare;
    [SerializeField] private Color platformColorInTriangle;
    
    private Color colorOfPlatforms;
    
    [Header("Colors for the player")]
    [SerializeField] private Color colorOfCircle;
    [SerializeField] private Color colorOfSquare;
    [SerializeField] private Color colorOfTriangle;
    
    private Color colorOfCurrentPlayer;

   
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

        // change color of current player
        _activePlayer.GetComponent<SpriteRenderer>().color = colorOfCurrentPlayer;
        
        // change colors of not moving platforms
        foreach (var platform in platforms)
        {
            platform.GetComponent<SpriteRenderer>().color = colorOfPlatforms;
            for (int i = 0; i < platform.transform.childCount; i++)
            {
                //platform.gameObject.transform.GetChild(i).GetComponent<Renderer>().material.color = colorOfPlatforms;
                platform.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = colorOfPlatforms;
            }
        }
        
    }

    public void ChangeToCircle()
    {
        BeforeChoosingActivePlayer(0);
        currentPlayerName = "Circle";

        colorOfCurrentPlayer = colorOfCircle;
        colorOfPlatforms = platformColorInCircle;
        AfterChoosingActivePlayer();
    }
    
    public void ChangeToSquare()
    {
        BeforeChoosingActivePlayer(1);
        currentPlayerName = "Square";

        colorOfCurrentPlayer = colorOfSquare;
        colorOfPlatforms = platformColorInSquare;
        AfterChoosingActivePlayer();
    }
    
    public void ChangeToTriangle()
    {
        BeforeChoosingActivePlayer(2);
        currentPlayerName = "Triangle";

        colorOfCurrentPlayer = colorOfTriangle;
        colorOfPlatforms = platformColorInTriangle;
        AfterChoosingActivePlayer();
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
        _activePlayer.transform.localPosition = GameManager.Manager.ReturnPoint;
        ActivePlayerScript.ResetMovement();
    }
    
    public void ToCircle(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            previousPlayerName = currentPlayerName;
            ChangeToCircle();
            GameManager.Manager.HasChangedToCircle = true;
            // _activeAnimator.SetBool("Circle", true);
            
            // optional change colors here
        }
    }
    
    public void ToSquare(InputAction.CallbackContext context)
    {
        if (context.performed && CanSquare)
        {
            previousPlayerName = currentPlayerName;
            ChangeToSquare();
            GameManager.Manager.HasChangedToSquare = true;
            // _activeAnimator.SetBool("Square", true);
        }
    }

    public void ToTriangle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            previousPlayerName = currentPlayerName;
            ChangeToTriangle();
            GameManager.Manager.HasChangedToTriangle = true;
            // _activeAnimator.SetBool("Triangle", true);
        }
    }
    

}
