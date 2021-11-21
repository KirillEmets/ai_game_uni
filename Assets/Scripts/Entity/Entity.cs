using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    
    public bool Invincible { get; set; }

    public void TakeDamage(float amount)
    {
        if(Invincible)
            return;

        Health = (int) math.max(0, Health - amount);
    }
}