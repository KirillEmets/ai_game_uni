using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

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