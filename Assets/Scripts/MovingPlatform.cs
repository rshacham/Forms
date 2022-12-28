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

    private RigidbodyInterpolation2D previousInterpolation;

    [SerializeField] private float bufferTime;
    private bool _afterStick;
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
        if (other.gameObject.tag == "players" && !_afterStick)
        {
            Debug.Log("enter");
            StickPlayer(other);
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("players") && !_afterStick && other.transform.parent == null)
        {
            Debug.Log("stay");
            StickPlayer(other);
        }
    }
    
    private void StickPlayer(Collision2D other)
    {
        previousInterpolation = PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation;
        _afterStick = true;
        PlayersManager.Manager.ActivePlayerScript.CanRotate(false);
        StartCoroutine(CancelAfterStick());
        PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation = RigidbodyInterpolation2D.None;
        other.transform.SetParent(transform, true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("players"))
        {
            PlayersManager.Manager.ActivePlayerScript.CanRotate(true);
            Debug.Log("exit");
            _afterStick = true;
            StartCoroutine(CancelAfterStick());
            PlayersManager.Manager.ActivePlayerScript.PlayerRigidBody.interpolation = previousInterpolation;
            other.transform.SetParent(null, true);
        }
    }

    IEnumerator CancelAfterStick()
    {
        yield return new WaitForSeconds(bufferTime);
        _afterStick = false;
    }
}