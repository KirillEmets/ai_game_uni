using System;

namespace Enemy
{
    public static class AI
    {
        public static EnemyBehaviour GetBehaviourType(AIType aiType, EnemyController controller) => aiType switch
        {
            AIType.Melee => new MeleeBehaviour(controller),
            AIType.Range => new MeleeBehaviour(controller),
            _ => throw new ArgumentOutOfRangeException()
        };

        public enum AIType
        {
            Melee,
            Range
        }
    }