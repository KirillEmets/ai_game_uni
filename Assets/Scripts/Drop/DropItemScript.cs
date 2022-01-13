using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum DropItemType
{
    Health, Arrows
}

public class DropItemScript : MonoBehaviour
{
    public Sprite arrowSprite;
    public Sprite healthSprite;

    private IDropItemBehaviour _behaviour;

    public void Init(DropItemType dropItemType, IDropItemBehaviour dropItemBehaviour)
    {
        GetComponent<SpriteRenderer>().sprite = dropItemType switch
        {
            DropItemType.Arrows => arrowSprite,
            DropItemType.Health => healthSprite,
            _ => null
        };
        
        _behaviour = dropItemBehaviour;
    }

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, 10f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;

        _behaviour.OnPickUp(other.GetComponent<PlayerController>());
        Destroy(gameObject);
    }
}