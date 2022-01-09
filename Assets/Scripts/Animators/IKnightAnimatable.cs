using System;

public interface IKnightAnimatable
{
    public event Action OnAttackStart;
    public event Action<Weapon> OnWeaponChange;

    public bool IsRunning();
    public int GetDirection();
}