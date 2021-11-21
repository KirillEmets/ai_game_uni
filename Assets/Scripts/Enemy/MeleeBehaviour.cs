using UnityEngine;

namespace Enemy
{
    public class MeleeBehaviour : EnemyBehaviour
    {
        public MeleeBehaviour(EnemyController controller) : base(controller)
        {
        }

        public override Vector2 GetVelocity(Entity target)
        {
            if (!Controller.PlayerDetected)
                return Vector2.zero;
            return (target.transform.position - Controller.transform.position).normalized * enemyObject.movementSpeed;
        }
    }
}