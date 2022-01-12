using System;
using System.Collections.Generic;
using UnityEngine;

public class KnightAnimator : MonoBehaviour
{
    public Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int Attack = Animator.StringToHash("attack");
    private int _direction = 1;

    private IKnightAnimatable Animatable { get; set; }
    public Entity animatable;


    private void Start()
    {
        Animatable = animatable as IKnightAnimatable;
        Animatable.OnAttackStart += OnAttackStart;
        Animatable.OnWeaponChange += OnWeaponChange;
        Debug.Log(Animatable.GetWeapon());
        OnWeaponChange(Animatable.GetWeapon());
    }

    private void OnWeaponChange(Weapon weapon)
    {
        transform.Find("Model").Find("body").Find("Weapon").GetComponentInChildren<WeaponSpriteChanger>()
            .ChangeWeapon(weapon);
    }

    private void OnAttackStart()
    {
        animator.SetTrigger(Attack);
    }

    void Update()
    {
        animator.SetBool(IsRunning, Animatable.IsRunning());

        var charDir = Animatable.GetDirection();
        if (_direction != charDir)
        {
            _direction = charDir;
            var scale = transform.localScale;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.localScale = new Vector3(charDir, scale.y, scale.z);
        }
    }
}