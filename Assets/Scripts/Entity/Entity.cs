using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    private float MaxHealth { get; set; }
    
    private float _health;
    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged.Invoke(_health, MaxHealth);
        }
    }

    public event Action<float, float> OnHealthChanged = delegate(float f, float f1) { };

    [FormerlySerializedAs("Stats")] public Stats stats;

    private bool Invincible { get; set; }


    public void Start()
    {
        MaxHealth = stats.health;
        Health = stats.health;
    }

    public void TakeDamage(float amount)
    {
        if (Invincible)
            return;

        Health = math.max(0, Health - amount);
    }
}