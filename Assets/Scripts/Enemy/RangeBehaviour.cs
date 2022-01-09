using System;
using System.Collections.Generic;
using UnityEngine;

public class RangeBehaviour : EnemyBehaviour
{
    public RangeBehaviour(EnemyController controller) : base(controller)
    {
        controller.attack = new RangeAttack();
    }

    public override void Update()
    {
        var pos = (Vector2) Controller.transform.position;
        var target = (Vector2) Player.transform.position;
        var moveDirection = Vector2.zero;
        if (Vector2.Distance(pos, target) <= 6)
        {
            moveDirection = (pos - target);

            var bestDistance = 0f;
            var bestDirection = Vector2.zero;

            var rotZ = Mathf.Atan2(moveDirection.y, moveDirection.x);
            for (var i = 0; i < 12; i++)
            {
                const float lookDistance = 5f;
                var direction = new Vector2(Mathf.Cos(i * Mathf.PI / 6 + rotZ), Mathf.Sin(i * Mathf.PI / 6 + rotZ));
                var hit = Physics2D.CircleCast(pos, 0.5f, direction, lookDistance, 1 << 9);

                var spareDistance = hit.collider == null ? lookDistance : hit.distance;
                Debug.DrawRay(pos, direction * spareDistance);

                var distanceToPlayer = Vector2.Distance(pos + direction * spareDistance, target);
                if (distanceToPlayer > bestDistance)
                {
                    bestDistance = distanceToPlayer;
                    bestDirection = direction;
                }
            }

            moveDirection = bestDirection;
        }

        Controller.SetVelocity(moveDirection.normalized * enemyObject.movementSpeed);
        Controller.StartAttack(new AttackParams(Controller, enemyObject, Player));
    }
}