using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedOfPlatform;
    [SerializeField] private float[] points;

    private int _index = 0;

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

        transform.localPosition = Vector2.MoveTowards(transform.localPosition, 
            new Vector2(points[_index], transform.localPosition.y), 
            speedOfPlatform * Time.deltaTime);
    }
    
    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     Debug.Log("Player Enter");
    //     other.transform.SetParent(transform);
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     Debug.Log("Player Out");
    //     other.transform.SetParent(null);
    // }
    //
    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("Player Enter");
    //     other.transform.SetParent(transform);
    // }
    //
    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     Debug.Log("Player Out");
    //     other.transform.SetParent(null);
    // }
}
