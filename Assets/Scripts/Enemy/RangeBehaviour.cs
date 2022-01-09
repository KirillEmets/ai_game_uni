using UnityEngine;

public class RangeBehaviour: EnemyBehaviour
{
    public RangeBehaviour(EnemyController controller) : base(controller)
    {
        controller.attack = new RangeAttack();
    }

    public override void Update()
    {
        var pos = Controller.transform.position;
        var target = Player.transform.position;
        var velocity = Vector2.zero;
        if (Vector2.Distance(pos, target) <= 6)
        {
            velocity = (pos - target).normalized * enemyObject.movementSpeed;
        }
        
        Controller.SetVelocity(velocity);
        Controller.StartAttack(new AttackParams(Controller, enemyObject, Player));
    }
}