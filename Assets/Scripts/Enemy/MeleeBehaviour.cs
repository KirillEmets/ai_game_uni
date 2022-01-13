using System;
using System.Linq;
using UnityEngine;


public class MeleeBehaviour : EnemyBehaviour
{
    // enum MeleeState
    // {
    //     GoToTarget,
    //     RunAway,
    //     Attacking
    // }
    //
    // private MeleeState State;

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
        var moveDirection = Vector2.zero;
        var myPos = (Vector2) Controller.transform.position;

        var rangeCohorts = Controller.Cohorts.FindAll(c => c != null && c.Weapon == Weapon.Bow);
        var playerPos = (Vector2) Player.transform.position;
        if (rangeCohorts.Count > 0)
        {
            var c = rangeCohorts[0];
            foreach (var rc in rangeCohorts)
            {
                if (Vector2.Distance(rc.transform.position, playerPos) <
                    Vector2.Distance(c.transform.position, playerPos))
                {
                    c = rc;
                }
            }

            var cpos = (Vector2) c.transform.position;

            switch (Player.Weapon)
            {
                case Weapon.Bow:
                    if (Player.ArrowCount == 0)
                    {
                        var targetPos = (cpos + (playerPos - cpos).normalized * 2f);
                        moveDirection = targetPos - myPos;
                    }
                    else
                        moveDirection = playerPos - myPos;

                    break;
                case Weapon.Sword:
                {
                    var targetPos = (cpos + (playerPos - cpos).normalized * 2f);
                    moveDirection = targetPos - myPos;
                    break;
                }
            }
        }
        else
        {
            moveDirection = playerPos - myPos;
        }

        if (moveDirection.magnitude < 0.8f) moveDirection = Vector2.zero;
        Controller.SetVelocity(moveDirection.normalized * enemyObject.movementSpeed);

        if (ShouldAttack())
        {
            Controller.StartAttack(new AttackParams(Controller, enemyObject, targetsMask: 1 << 8));
        }
    }

    public bool ShouldAttack() =>
        Vector2.Distance(Controller.transform.position, Player.transform.position) <=
        Controller.EnemyObject.attackDistance;
}