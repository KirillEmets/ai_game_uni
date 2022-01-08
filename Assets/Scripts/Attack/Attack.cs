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
    
    protected virtual void PerformOnPosition(Entity entity, Stats stats, Vector2 position)
    {
    }

    public void Perform(Entity entity, Stats stats, Entity target = null, Vector2? targetPosition = null, int? targetsMask = null)
    {
        if (!IsReady()) return;

        nextAttackTime = Time.time + GetCooldown() / stats.attackSpeed;
        if (target != null)
        {
            PerformTarget(entity, stats, target);
        }
        else if(targetPosition.HasValue)
        {
            PerformOnPosition(entity, stats, targetPosition.Value);
        }
        else if(targetsMask.HasValue)
        {
            PerformNoTarget(entity, stats, targetsMask.Value);
        }

        Preserved = false;
    }

    public void Preserve()
    {
        Preserved = true;
    }

    public bool IsReady() => Time.time >= nextAttackTime;
}