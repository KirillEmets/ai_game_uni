using UnityEngine;


public abstract class EnemyBehaviour
{
    protected EnemyController Controller { get; }
    protected readonly EnemyObject enemyObject;
    public PlayerController Player { get; set; }


    protected EnemyBehaviour(EnemyController controller)
    {
        Controller = controller;
        enemyObject = controller.EnemyObject;
    }

    public abstract void Update();
}