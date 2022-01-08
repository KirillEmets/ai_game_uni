using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity, IKnightAnimatable
{
    
    public override int GetLayerMask() => 1 << 8;
    private const int EnemyTargetMask = ~(1 << 8); 

    private Rigidbody2D Rb { get; set; }
    internal Vector2 Velocity { get; private set; }

    private Vector2 _refVelocity = Vector2.zero;

    public Attack attack;

    new void Start()
    {
        base.Start();
        Rb = GetComponent<Rigidbody2D>();
        attack = new RangeAttack();
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
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        StartAttack(new AttackParams(this, stats, mousePos, EnemyTargetMask));
    }

    public void StartAttack(AttackParams attackParams)
    {
        if (!attack.IsReady() || attack.Preserved) return;

        attack.Preserve();
        OnAttackStart.Invoke();
        StartCoroutine(nameof(WaitAndPerformAttack), attackParams);
    }

    IEnumerator WaitAndPerformAttack(AttackParams attackParams)
    {
        yield return new WaitForSeconds(0.3f);
        attack.Perform(attackParams);
    }
    
    public event Action OnAttackStart = delegate { };
    public bool IsRunning() => Velocity.magnitude > 0.1f;

    public int GetDirection()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirectionX = mousePos.x - transform.position.x;
        return mouseDirectionX < 0 ? 1 : -1;
    }
}