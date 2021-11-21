using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private Rigidbody2D Rb { get; set; }
    internal Vector2 Velocity { get; private set; }
    public float speed;

    private Vector2 _refVelocity = Vector2.zero;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");
        var direction = new Vector2(horizontal, vertical);
        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }

        Velocity = Vector2.SmoothDamp(Velocity, direction * speed, ref _refVelocity, 0.1f);
        Rb.velocity = Velocity;
    }
}