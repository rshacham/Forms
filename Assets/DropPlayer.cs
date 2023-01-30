using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private GameObject plaftorm;

    [SerializeField] private int gotToCircleTarget;
    private int _gotToCircleCounter = 0;

    private void Start()
    {
        _player = FindObjectOfType<CirclePlayer>().gameObject;
    }

    private void Update()
    {
        if (_gotToCircleCounter == gotToCircleTarget)
        {
            _gotToCircleCounter++;
            Drop();
        }
    }

    public void GotToCircle()
    {
        _gotToCircleCounter++;
    }

    private void Drop()
    {
        _player.GetComponent<SpriteRenderer>().color = Color.white;
        plaftorm.SetActive(false);
        _player.GetComponent<CirclePlayer>().CanMove = true;
    }
}
