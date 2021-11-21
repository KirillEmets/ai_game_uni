using System;
using System.Collections;
using System.Collections.Generic;
using Enemy;
using Unity.Mathematics;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float MaxHealth { get; set; }
    
    public float Health;
    public Stats Stats;
    
    public bool Invincible { get; set; }


    public void Start()
    {
        MaxHealth = Stats.health;
        Health = Stats.health;
    }

    public void TakeDamage(float amount)
    {
        if(Invincible)
            return;

        Health = math.max(0, Health - amount);
    }
}