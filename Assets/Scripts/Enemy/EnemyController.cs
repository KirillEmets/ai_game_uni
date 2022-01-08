using UnityEngine;


public class EnemyController : Entity
{
    const int PlayerLayerMask = ~(1 << 7);

    public EnemyObject enemyObject;

    private EnemyBehaviour EnemyBehaviour { get; set; }

    public bool PlayerDetected { get; private set; }
    private PlayerController Player { get; set; }

    private Rigidbody2D Rb { get; set; }

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
}