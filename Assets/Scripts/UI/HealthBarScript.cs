using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Image foregroundImage;

    private void Start()
    {
        GetComponentInParent<Entity>().OnHealthChanged += HandleHealthChanged;
    }

    void HandleHealthChanged(float current, float max)
    {
        foregroundImage.fillAmount = current / max;
    }
}
