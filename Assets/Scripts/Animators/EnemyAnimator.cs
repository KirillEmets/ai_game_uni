public class EnemyAnimator : KnightAnimator
{
    public EnemyController enemyController;
    public override bool GetIsRunning() => enemyController.GetVelocity().magnitude > 0.1f;

    public override int GetCharDirection()
    {
        return enemyController.GetVelocity().x < 0 ? 1 : -1;
    }
}