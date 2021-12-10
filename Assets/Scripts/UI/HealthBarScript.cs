using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image foregroundImage;
    public Entity targetEntity;

    public void Bind(Entity entity)
    {
        targetEntity = entity;
        targetEntity.OnHealthChanged += HandleHealthChanged;
    }

    private void Update()
    {
        if(targetEntity == null)
            return;
        transform.position = (Vector2) targetEntity.transform.position + Vector2.up * 1.5f;
    }

    void HandleHealthChanged(float current, float max)
    {
        foregroundImage.fillAmount = current / max;
    }

    private void OnDestroy()
    {
        targetEntity.OnHealthChanged -= HandleHealthChanged;
    }
}