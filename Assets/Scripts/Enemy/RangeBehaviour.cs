using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangeBehaviour : EnemyBehaviour
{
    public RangeBehaviour(EnemyController controller) : base(controller)
    {
        controller.attack = new RangeAttack();
    }

    public override void Update()
    {
        var myPos = (Vector2) Controller.transform.position;
        var playerPos = (Vector2) Player.transform.position;
        var moveDirection = Vector2.zero;

        if (Vector2.Distance(myPos, playerPos) <= 6)
        {
            moveDirection = (myPos - playerPos);

            var bestDistance = 0f;
            var bestDirection = Vector2.zero;

            var rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x);
            for (var i = 0; i < 12; i++)
            {
                const float lookDistance = 5f;
                var direction = new Vector2(Mathf.Cos(i * Mathf.PI / 6 + rotZ), Mathf.Sin(i * Mathf.PI / 6 + rotZ));
                var hit = Physics2D.CircleCast(myPos, 0.5f, direction, lookDistance, 1 << 9);

                var spareDistance = hit.collider == null ? lookDistance : hit.distance;
                Debug.DrawRay(myPos, direction * spareDistance);

                var distanceToPlayer = Vector2.Distance(myPos + direction * spareDistance, playerPos);
                if (distanceToPlayer > bestDistance)
                {
                    bestDistance = distanceToPlayer;
                    bestDirection = direction;
                }
            }

            moveDirection = bestDirection;
        }

        var rangeCohorts = Controller.Cohorts.FindAll(c => c != null && c.Weapon == Weapon.Bow);
        if (rangeCohorts.Count > 0)
        {
            var center = rangeCohorts.Aggregate(Vector2.zero,
                (current, rc) => current + (Vector2) rc.transform.position / rangeCohorts.Count);

            moveDirection += center - myPos;
        }

        if(moveDirection.magnitude < 0.8f) moveDirection = Vector2.zero;

        Controller.SetVelocity(moveDirection.normalized * enemyObject.movementSpeed);
        Controller.StartAttack(new AttackParams(Controller, enemyObject, Player));
    }
}