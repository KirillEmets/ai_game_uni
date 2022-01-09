using UnityEngine;

public class RangeAttack : Attack
{
    public override string GetName() => "range";

    protected override float GetCooldown() => 1f;

    protected override void PerformTarget(Entity entity, Stats stats, Entity target)
    {
        PerformOnPosition(entity, stats, target.transform.position, ~entity.GetLayerMask());
    }

    protected override void PerformOnPosition(Entity entity, Stats stats, Vector2 position, int targetsMask)
    {
        entity.projectileManager.CreateArrow(
            entity.gameObject,
            stats.damage,
            position - (Vector2) entity.transform.position,
            targetsMask
        );
    }
}