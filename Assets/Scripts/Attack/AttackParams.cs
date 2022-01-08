using UnityEngine;

public class AttackParams
{
    public Entity entity;
    public Stats stats;
    public Entity target = null;
    public Vector2? targetPosition = null;
    public int? targetsMask = null;

    public AttackParams(Entity entity, Stats stats, Entity target)
    {
        this.entity = entity;
        this.stats = stats;
        this.target = target;
    }

    public AttackParams(Entity entity, Stats stats, Vector2 targetPosition)
    {
        this.entity = entity;
        this.stats = stats;
        this.targetPosition = targetPosition;
    }

    public AttackParams(Entity entity, Stats stats, int targetsMask)
    {
        this.entity = entity;
        this.stats = stats;
        this.targetsMask = targetsMask;
    }
}