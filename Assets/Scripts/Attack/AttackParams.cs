using UnityEngine;


public class AttackParams
{
    public readonly Entity entity;
    public readonly Stats stats;
    public readonly Entity target = null;
    public Vector2? targetPosition = null;
    public int? targetsMask = null;

    public AttackParams(Entity entity, Stats stats, Entity target)
    {
        this.entity = entity;
        this.stats = stats;
        this.target = target;
    }

    public AttackParams(Entity entity, Stats stats, Vector2 targetPosition, int targetsMask)
    {
        this.entity = entity;
        this.stats = stats;
        this.targetPosition = targetPosition;
        this.targetsMask = targetsMask;
    }

    public AttackParams(Entity entity, Stats stats, int targetsMask)
    {
        this.entity = entity;
        this.stats = stats;
        this.targetsMask = targetsMask;
    }
}