using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class KnightAnimator : MonoBehaviour
{
    public Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int Attack = Animator.StringToHash("attack");
    private int _direction = 1;

    public void OnAttackStart()
    {
        animator.SetTrigger(Attack);
    }

    void Update()
    {
        animator.SetBool(IsRunning, GetIsRunning());
        
        var charDir = GetCharDirection();
        if (_direction != charDir)
        {
            _direction = charDir;
            var scale = transform.localScale;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.localScale = new Vector3(charDir, scale.y, scale.z);
        }
    }

    public abstract bool GetIsRunning();
    public abstract int GetCharDirection();
}

public class EnemyAnimator : KnightAnimator
{

    public EnemyController enemyController;
    public override bool GetIsRunning() => enemyController.GetVelocity().magnitude > 0.1f;

    public override int GetCharDirection()
    {
        return  enemyController.GetVelocity().x < 0 ? 1 : -1;
    }
}

public class PlayerAnimator : KnightAnimator
{
    public PlayerController playerController;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
        playerController.OnAttackStart += OnAttackStart;
    }

    public override bool GetIsRunning() => playerController.Velocity.magnitude > 0.1f;

    public override int GetCharDirection()
    {
        var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var mouseDirectionX = mousePos.x - transform.position.x;
        return mouseDirectionX < 0 ? 1 : -1;
    }
}