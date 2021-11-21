using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using UnityEngine;

public class PlayerController : Entity
{
    private Rigidbody2D Rb { get; set; }
    internal Vector2 Velocity { get; private set; }

    private Vector2 _refVelocity = Vector2.zero;


    new void Start()
    {
        Debug.Log("KEk");
        base.Start();
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

        Velocity = Vector2.SmoothDamp(Velocity, direction * Stats.movementSpeed, ref _refVelocity, 0.1f);
        Rb.velocity = Velocity;
    }
}