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

        public virtual void Perform(Entity entity, Stats stats, Entity target, int targetsMask)
        {
            if(!IsReady())
                return;

            nextAttackTime = Time.time + GetCooldown();
            if (target != null)
            {
                PerformTarget(entity, stats, target);
            }
            else
            {
                PerformNoTarget(entity, stats, targetsMask);
            }
        }

        public bool IsReady() => Time.time >= nextAttackTime;
    }

    public class MeleeAoEAttack : Attack
    {
        private readonly Collider2D[] _targets = new Collider2D[3];

        public override string GetName() => "melee_aoe";

        protected override float GetCooldown() => 1f;

        protected override void PerformNoTarget(Entity entity, Stats stats, int targetsMask)
        {
            Physics2D.OverlapCircleNonAlloc(
                entity.transform.position,
                stats.attackDistance,
                _targets,
                targetsMask
            );

            foreach (var col in _targets)
            {
                if (col != null)
                    col.GetComponent<Entity>().TakeDamage(stats.damage);
            }
        }
    }

    public abstract class AttackBehaviour
    {
        public abstract bool ShouldAttack(EnemyController attacker, Entity target);
    }

    public class MeleeAttackBehaviour : AttackBehaviour
    {
        public override bool ShouldAttack(EnemyController attacker, Entity target) =>
            Vector2.Distance(attacker.transform.position, target.transform.position) <
            attacker.enemyObject.attackDistance * 0.7f;
    }
}