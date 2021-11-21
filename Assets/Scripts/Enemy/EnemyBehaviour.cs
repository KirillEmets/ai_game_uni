using UnityEngine;

namespace Enemy
{
    public abstract class EnemyBehaviour
    {
        protected EnemyController Controller { get; }
        protected readonly EnemyObject enemyObject;

        protected EnemyBehaviour(EnemyController controller)
        {
            Controller = controller;
            enemyObject = controller.enemyObject;
        }

        public abstract Vector2 GetVelocity(Entity target);
    }
}