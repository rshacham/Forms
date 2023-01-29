using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Curve : MonoBehaviour
{
    [SerializeField] private float forcePower;
    
    [SerializeField] private Vector2 forceAtExit;
    private Collider _myCollider;

    private bool _happend;
    private void OnCollisionEnter2D(Collision2D other)
    {
        var playerDirection = other.rigidbody.velocity.x;
        foreach (var item in other.contacts)
        {
            Debug.DrawRay(item.point, item.normal * 100, Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f), 10f);
            print(item.normal);

            var force = RotateNormalWithMovement(item.normal, playerDirection) * forcePower;
            print(force);
            if (!_happend)
            {
                other.rigidbody.AddForce(force, ForceMode2D.Impulse);
                _happend = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        print("exit");
        other.rigidbody.AddForce(forceAtExit);
    }

    private Vector3 RotateNormalWithMovement(Vector3 normal, float direction)
    {
        if (direction < -5)
        {
            return new Vector3(normal.y, -normal.x);
        }

        return new Vector3(-normal.y, normal.x);
    }
}
