using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : Entity, IKnightAnimatable
{
    public override int GetLayerMask() => 1 << 7;

    const int PlayerLayerMask = ~(1 << 7);

    public EnemyObject EnemyObject { get; set; }

    private EnemyBehaviour EnemyBehaviour { get; set; }

    public bool PlayerDetected { get; private set; }
    private PlayerController Player { get; set; }

    private Rigidbody2D Rb { get; set; }

    public Attack attack;

    public Weapon Weapon { get; set; }

    public List<EnemyController> Cohorts { get; private set; } = new List<EnemyController>();
    
    private void Awake()
    {
        EnemyObject = stats as EnemyObject;
    }

    public void Init(Weapon weapon, IEnumerable<EnemyController> cohorts)
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        ChangeWeapon(weapon);
        AddCohorts(cohorts);
    }

    public void ChangeWeapon(Weapon weapon)
    {
        Weapon = weapon;

        if (weapon == Weapon.Sword)
            EnemyBehaviour = new MeleeBehaviour(this);
        else
            EnemyBehaviour = new RangeBehaviour(this);

        EnemyBehaviour.Player = Player;

        OnWeaponChange(weapon);
    }
    
    private new void Start()
    {
        base.Start();
        Rb = GetComponent<Rigidbody2D>();
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

    public void AddCohorts(IEnumerable<EnemyController> cohorts)
    {
        Cohorts.AddRange(cohorts);
    }

    private void DetectPlayer()
    {
        if (PlayerDetected) return;

        var pos = transform.position;

        var hit = Physics2D.Raycast(pos, Player.transform.position - pos, EnemyObject.detectionDistance,
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
    public event Action<Weapon> OnWeaponChange = delegate {  };
    public bool IsRunning() => GetVelocity().magnitude > 0.3f;
    public int GetDirection() => Math.Sign((transform.position - Player.transform.position).x);
    public Weapon GetWeapon() => Weapon;
}