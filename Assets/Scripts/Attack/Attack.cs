using UnityEngine;


public abstract class Attack
{
    public abstract string GetName();
    protected abstract float GetCooldown();

    private float nextAttackTime = 0;
    public bool Preserved { get; private set; }

    protected virtual void PerformTarget(Entity entity, Stats stats, Entity target)
    {
    }

    protected virtual void PerformNoTarget(Entity entity, Stats stats, int targetsMask)
    {
    }

    protected virtual void PerformOnPosition(Entity entity, Stats stats, Vector2 position, int targetsMask)
    {
    }

    public void Perform(AttackParams attackParams)
    {
        if (!IsReady()) return;

        nextAttackTime = Time.time + GetCooldown() / attackParams.stats.attackSpeed;
        if (attackParams.target != null)
        {
            PerformTarget(attackParams.entity, attackParams.stats, attackParams.target);
        }
        else if (attackParams.targetPosition.HasValue && attackParams.targetsMask.HasValue)
        {
            PerformOnPosition(attackParams.entity, attackParams.stats, attackParams.targetPosition.Value,
                attackParams.targetsMask.Value);
        }
        else if (attackParams.targetsMask.HasValue)
        {
            PerformNoTarget(attackParams.entity, attackParams.stats, attackParams.targetsMask.Value);
        }

        Preserved = false;
    }

    public void Preserve()
    {
        Preserved = true;
    }

    public bool IsReady() => Time.time >= nextAttackTime;
}