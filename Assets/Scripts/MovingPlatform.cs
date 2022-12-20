using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speedOfPlatform;
    [SerializeField] private Vector2[] points;

    private int _index = 0;

    private void Update()
    {
        if (Vector2.Distance(transform.position, points[_index]) < 0.02f)
        {
            _index++;
            if (_index == points.Length)
            {
                _index = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, 
                                                        points[_index], 
                                                         speedOfPlatform * Time.deltaTime);
    }
}
