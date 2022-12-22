using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedOfPlatform;
    [SerializeField] private float[] points;

    private float _lastFrameMovement;

    private Vector2 _lastFramePosition;
    private Vector2 _distanceToNewPosition;

    private int _index = 0;

    private bool _playerOnPlatform = false;

    private void Update()
    {
        if (Vector2.Distance(transform.localPosition, new Vector2(points[_index], transform.localPosition.y)) < 0.02f)
        {
            _index++;
            if (_index == points.Length)
            {
                _index = 0;
            }
        }

        Vector2 newPosition = Vector2.MoveTowards(transform.localPosition,
            new Vector2(points[_index], transform.localPosition.y),
            speedOfPlatform * Time.deltaTime);

        transform.localPosition = newPosition;

        if (_playerOnPlatform)
        {
            PlayersManager.Manager.PlatformFactor = newPosition - _lastFramePosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.transform.SetParent(transform, true);
        PlayersManager.Manager.ActivePlayerScript.CanRotate(false);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.SetParent(null, true);
        PlayersManager.Manager.ActivePlayerScript.CanRotate(true);
    }
}

