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

        private MeleeState State;

        private MeleeAoEAttack AoEAttack = new MeleeAoEAttack();
        public MeleeBehaviour(EnemyController controller) : base(controller)
        {
            
        }

        private Vector2 GetVelocity(Component target)
        {
            if (!Controller.PlayerDetected)
                return Vector2.zero;
            return (target.transform.position - Controller.transform.position).normalized * enemyObject.movementSpeed;
        }

        public override void Update()
        {
            Controller.SetVelocity(GetVelocity(Player));

            if (ShouldAttack())
            {
                AoEAttack.Perform(Controller, enemyObject, ~(1 << 7));
            }
        }

        public bool ShouldAttack() =>
            Vector2.Distance(Controller.transform.position, Player.transform.position) <
            Controller.enemyObject.attackDistance * 0.7f;
    }
}