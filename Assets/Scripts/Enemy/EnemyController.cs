using System;
using System.Collections;
using UnityEngine;


public class EnemyController : Entity, IKnightAnimatable
{
    const int PlayerLayerMask = ~(1 << 7);

    public EnemyObject enemyObject;

    private EnemyBehaviour EnemyBehaviour { get; set; }

    public bool PlayerDetected { get; private set; }
    private PlayerController Player { get; set; }

    private Rigidbody2D Rb { get; set; }

    public Attack attack;

    private void Start()
    {
        base.Start();
        Rb = GetComponent<Rigidbody2D>();
        EnemyBehaviour = AI.GetBehaviourType(enemyObject.aiType, this);
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        EnemyBehaviour.Player = Player;
    }

    private void Update()
    {
        if (!PlayerDetected)
            DetectPlayer();

        EnemyBehaviour.Update();
    }

    public void SetVelocity(Vector2 velocity)
    {
        Rb.velocity = velocity;
    }

    public Vector2 GetVelocity() => Rb.velocity;

    private void DetectPlayer()
    {
        if (PlayerDetected) return;

        var pos = transform.position;

        var hit = Physics2D.Raycast(pos, Player.transform.position - pos, enemyObject.detectionDistance,
            PlayerLayerMask);
        var hitCollider = hit.collider;
        if ((object) hitCollider != null && hitCollider.CompareTag("Player"))
        {
            PlayerDetected = true;
        }
    }

    public void StartAttack(AttackParams attackParams)
    {
        if (!attack.IsReady() || attack.Preserved) return;

        attack.Preserve();
        OnAttackStart.Invoke();
        StartCoroutine(nameof(WaitAndPerformAttack), attackParams);
    }

    IEnumerator WaitAndPerformAttack(AttackParams attackParams)
    {
        yield return new WaitForSeconds(0.3f);
        attack.Perform(attackParams);
    }

    public event Action OnAttackStart = delegate { };
    public bool IsRunning() => GetVelocity().magnitude > 0.1f;
    public int GetDirection() => GetVelocity().x < 0 ? 1 : -1;
}