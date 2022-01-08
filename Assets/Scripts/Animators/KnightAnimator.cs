using UnityEngine;

public abstract class KnightAnimator : MonoBehaviour
{
    public Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int Attack = Animator.StringToHash("attack");
    private int _direction = 1;

    public void OnAttackStart()
    {
        animator.SetTrigger(Attack);
    }

    void Update()
    {
        animator.SetBool(IsRunning, GetIsRunning());
        
        var charDir = GetCharDirection();
        if (_direction != charDir)
        {
            _direction = charDir;
            var scale = transform.localScale;
            // ReSharper disable once Unity.InefficientPropertyAccess
            transform.localScale = new Vector3(charDir, scale.y, scale.z);
        }
    }

    public abstract bool GetIsRunning();
    public abstract int GetCharDirection();
}