﻿using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Entity : MonoBehaviour
{
    private float MaxHealth { get; set; }

    public float _health;

    private float Health
    {
        get => _health;
        set
        {
            _health = value;
            OnHealthChanged.Invoke(_health, MaxHealth);
        }
    }

    public GameObject healthBarPrefab;
    public HealthBarScript healthBarScript;
    public ProjectileManager projectileManager;

    public event Action<float, float> OnHealthChanged = delegate { };

    [FormerlySerializedAs("Stats")] public Stats stats;

    private bool Invincible { get; set; }
    
    public void Start()
    {
        MaxHealth = stats.health;
        Health = stats.health;

        healthBarScript = Instantiate(healthBarPrefab).GetComponent<HealthBarScript>();
        healthBarScript.Bind(this);

        projectileManager = GameObject.FindWithTag("ProjectileManager").GetComponent<ProjectileManager>();
    }

    private void OnDestroy()
    {
        if (healthBarScript != null)
            Destroy(healthBarScript.gameObject);
    }

    public void TakeDamage(float amount)
    {
        if (Invincible)
            return;

        Health = math.max(0, Health - amount);
    }
}