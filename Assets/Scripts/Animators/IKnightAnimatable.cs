using System;

public interface IKnightAnimatable
{
    public event Action OnAttackStart;

    public bool IsRunning();
    public int GetDirection();
}