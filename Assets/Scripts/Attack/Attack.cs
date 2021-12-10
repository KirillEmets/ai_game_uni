using UnityEngine;

namespace Enemy
{
    public abstract class Attack
    {
        public abstract string GetName();
        protected abstract float GetCooldown();

        private float nextAttackTime = 0;

        protected virtual void PerformTarget(Entity entity, Stats stats, Entity target)
        {
            
        }

        protected virtual void PerformNoTarget(Entity entity, Stats stats, int targetsMask)
        {
            
        }

        public bool Perform(Entity entity, Stats stats, Entity target, int targetsMask)
        {
            if(!IsReady())
                return false;

            nextAttackTime = Time.time + GetCooldown();
            if (target != null)
            {
                PerformTarget(entity, stats, target);
            }
            else
            {
                PerformNoTarget(entity, stats, targetsMask);
            }

            return true;
        }

        public bool IsReady() => Time.time >= nextAttackTime;
    }
}