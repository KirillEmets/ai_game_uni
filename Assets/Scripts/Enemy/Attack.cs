using UnityEngine;

namespace Enemy
{
    public abstract class Attack
    {
        public abstract void Perform(Entity entity, Stats stats, int targetsMask);
    }

    public class MeleeAoEAttack : Attack
    {
        private readonly Collider2D[] _targets = new Collider2D[3];

        public override void Perform(Entity entity, Stats stats, int targetsMask)
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