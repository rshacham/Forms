using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    private GameObject _activePlayer;
    private Player _activePlayerScript;
    private Transform _activePlayerTransform;
    
    
    private int _currentPlayer = 0;

    private bool changedShape = false;

    public bool HasChangedShape
    {
        get { return changedShape; }
        set { changedShape = value; }
    }
    
    private bool jumped = false;
    
    public bool HasJumped {
        get { return jumped; }
        set { jumped = value; }
    }
    
    void Start()
    {
        _activePlayer = players[_currentPlayer];
        _activePlayerTransform = _activePlayer.GetComponent<Transform>();
        UpdatePlayerScript();
    }
    
    void Update()
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

    public void UpdatePlayerScript()
    {
        switch (_currentPlayer % 3)
        {
            case 0:
                _activePlayerScript = _activePlayer.GetComponent<Player>();
                break;
            case 1:
                _activePlayerScript = _activePlayer.GetComponent<SquarePlayer>();
                break;
            case 2:
                _activePlayerScript = _activePlayer.GetComponent<TrianglePlayer>();
                break;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        HasJumped = true;
        _activePlayerScript.Jump(context);
    }
    
    
    // method to change position of the player (according to the coordinates in gameManger
    // calls the resetMovement (method in Player script)
}
