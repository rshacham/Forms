using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DropPlayer : MonoBehaviour
{
    private GameObject _player;
    private Transform _playerTransform;
    private SpriteRenderer _playerRenderer;
    
    [SerializeField] private GameObject plaftorm;

    [SerializeField] private int gotToCircleTarget;
    private int _gotToCircleCounter = 0;

    private bool buttonPressed;
    private bool soundPlayed;
    private bool playerDropped;

    [SerializeField] private AudioClip buzzSound;

    [SerializeField] private Color circleColor;
    private float startingHeight;
    [SerializeField] private float endingHeight;

    private void Start()
    {
        _player = FindObjectOfType<CirclePlayer>().gameObject;
        _playerTransform = _player.GetComponent<Transform>();
        _playerRenderer = _player.GetComponent<SpriteRenderer>();
        _player.GetComponent<CirclePlayer>().CanMove = false;
        startingHeight = _playerTransform.position.y;
    }

    private void Update()
    {
        if (playerDropped)
        {
            print(startingHeight);
            print(endingHeight);
            print(_playerTransform.position.y);

            var colorValue = Mathf.InverseLerp(startingHeight, endingHeight, _playerTransform.position.y);
            var colorForPlayer = Color.Lerp(Color.white, circleColor, colorValue);
            _playerRenderer.color = colorForPlayer;
        }
        
        if (_gotToCircleCounter == gotToCircleTarget)
        {
            _gotToCircleCounter++;
            Drop();
        }

        if (Input.GetJoystickNames().Length > 0)
        {
            var gamepadButtonPressed = Gamepad.current.allControls.Any(x => x is ButtonControl button && x.IsPressed() && !x.synthetic);
            buttonPressed = gamepadButtonPressed;
        }
    }

    public void GotToCircle()
    {
        if (soundPlayed && !playerDropped)
        {
            Drop();
        }
    }

    public void ButtonPressed()
    {
        buttonPressed = true;
    }

    public void PlayDropSound()
    {
        if (buttonPressed && !soundPlayed)
        {
            soundPlayed = true;
            SoundManager.Manager.PlaySound(buzzSound);
        }
    }

    private void Drop()
    {
        _player.GetComponent<SpriteRenderer>().color = Color.white;
        plaftorm.SetActive(false);
        _player.GetComponent<CirclePlayer>().CanMove = true;
        playerDropped = true;
        FindObjectOfType<DropPlayer>().gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
    }
}
