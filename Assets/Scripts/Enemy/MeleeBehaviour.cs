using UnityEngine;


public class MeleeBehaviour : EnemyBehaviour
{
    enum MeleeState
    {
        GoToTarget,
        RunAway,
        Attacking
    }

    private MeleeState State;

    public MeleeBehaviour(EnemyController controller) : base(controller)
    {
        controller.attack = new MeleeAoEAttack();
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
            Controller.StartAttack(new AttackParams(Controller, enemyObject, targetsMask: 1 << 8));
        }
    }

    public bool ShouldAttack() =>
        Vector2.Distance(Controller.transform.position, Player.transform.position) <=
        Controller.EnemyObject.attackDistance;
}