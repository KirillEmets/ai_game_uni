using UnityEngine;

namespace Enemy
{
    public class MeleeBehaviour : EnemyBehaviour
    {
        enum MeleeState
        {
            GoToTarget,
            RunAway,
            Attacking
        }       
       
        public MeleeBehaviour(EnemyController controller) : base(controller)
        {
            
        }

        private Vector2 GetVelocity(Entity target)
        {
            if (!Controller.PlayerDetected)
                return Vector2.zero;
            return (target.transform.position - Controller.transform.position).normalized * enemyObject.movementSpeed;
        }

        public override void Update()
        {
            EnemyController.SetVelocity(GetVelocity(Player));
        }
    }
}