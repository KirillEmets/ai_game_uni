using UnityEngine;

namespace Enemy
{
    public class MeleeAoEAttack : Attack
    {
        private readonly Collider2D[] _targets = new Collider2D[3];

        public override string GetName() => "melee_aoe";

        protected override float GetCooldown() => 1f;

        protected override void PerformNoTarget(Entity entity, Stats stats, int targetsMask)
        {
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i] = null;
            }
            
            Physics2D.OverlapCircleNonAlloc(
                entity.transform.position,
                stats.attackDistance,
                _targets,
                targetsMask
            );
            
            Debug.Log("kek " + Time.time);
            foreach (var col in _targets)
            {
                if (col != null)
                    col.GetComponent<Entity>().TakeDamage(stats.damage);
            }
        }
    }
}