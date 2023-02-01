using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedOfPlatform;
    [SerializeField] private Transform[] points;
    private List<Vector2> positions;
    private float _lastFrameMovement;
    private Vector2 _lastFramePosition;
    private Vector2 _distanceToNewPosition;
    private int _index = 0;
    private bool _playerOnPlatform = false;

    private Transform _playerTransform;

    private RigidbodyInterpolation2D previousInterpolation;
    
    [SerializeField] private bool oneTime;
    private bool _reachedEnd = false;

    #region New Solution
    [SerializeField] private Transform _holder;
    #endregion

    private void Start()
    {
        positions = new List<Vector2>();
        foreach (var point in points)
        {
            positions.Add(point.position);
        }
    }

    private void Update()
    {
        if (_holder == null || _reachedEnd)
        {
            return;
        }

        if (oneTime && _index >= points.Length)
        {
            return;
        }
        
        if (Vector2.Distance(transform.position, positions[_index]) < 0.02f)
        {
            _index++;
            if (_index == points.Length)
            {
                if (!oneTime)
                {
                    _index = 0;    
                }
            }
        }
        
        if (_index >= points.Length)
        {
            _reachedEnd = true;
            return;
        }

        Vector2 newPosition = Vector2.MoveTowards(transform.position,
            positions[_index],
            speedOfPlatform * Time.deltaTime);
        _holder.position = newPosition;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _playerTransform = other.transform;
        if (other.gameObject.tag == "players")
        {
            StickPlayer();
        }
    }

    private void StickPlayer()
    {
        previousInterpolation = PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation;
        PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation = RigidbodyInterpolation2D.None;
        _playerTransform.SetParent(_holder, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("players"))
        {
            UnStick();
        }
    }

    private void UnStick()
    {
        PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation = previousInterpolation;
        _playerTransform.SetParent(null, true);
    }
}