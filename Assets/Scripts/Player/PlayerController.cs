using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    private Rigidbody2D Rb { get; set; }
    internal Vector2 Velocity { get; private set; }

    private Vector2 _refVelocity = Vector2.zero;
    public event Action OnAttackStart = delegate { };

    public Attack attack;

    new void Start()
    {
        Debug.Log("KEk");
        base.Start();
        Rb = GetComponent<Rigidbody2D>();
        attack = new MeleeAoEAttack();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseClick();
        }
        
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");
        var direction = new Vector2(horizontal, vertical);
        if (direction.magnitude > 1f)
        {
            direction.Normalize();
        }

        Velocity = Vector2.SmoothDamp(Velocity, direction * stats.movementSpeed, ref _refVelocity, 0.1f);
        Rb.velocity = Velocity;
    }

    void OnMouseClick()
    {
        Attack();
    }

    void Attack()
    {
        if (attack.IsReady())
        {
            OnAttackStart.Invoke();
            StartCoroutine(nameof(WaitAndPerformAttack));
        }
    }

    IEnumerator WaitAndPerformAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attack.Perform(this, stats, null, ~(1 << 8));
    }
}