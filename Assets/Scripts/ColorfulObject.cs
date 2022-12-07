using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorfulObject : MonoBehaviour
{
    public Rigidbody2D _rigidbody2D;
    private Collider2D _collider;
    
    private float defaultGravity;

    [SerializeField] private GameManager.Colors color;

    private GameManager GameManager;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        defaultGravity = _rigidbody2D.gravityScale;
        GameManager.colorfulObjectsMap[color].Add(this);
    }

    public void ResetAbility()
    {
        _rigidbody2D.gravityScale = defaultGravity;
    }
}
