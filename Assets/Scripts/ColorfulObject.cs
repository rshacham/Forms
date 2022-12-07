using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorfulObject : MonoBehaviour
{
    public Rigidbody2D _rigidbody2D;

    private Collider2D _collider;

    [SerializeField] private GameManager.Colors color;

    private GameManager GameManager;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        GameManager.colorfulObjectsMap[color].Add(this);
    }
}
