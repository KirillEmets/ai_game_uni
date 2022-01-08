using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector2 Direction { get; set; } = Vector2.zero;
    private Collider2D Collider { get; set; }
    private GameObject Owner { get; set; }
    private int TargetsMask { get; set; }
    private float Damage { get; set; }

    public void Init(GameObject owner, float damage, Vector2 direction, int targetsMask)
    {
        Owner = owner;
        Direction = direction.normalized;
        transform.LookAt(transform.position + (Vector3) direction);
        TargetsMask = targetsMask;
        Damage = damage;

        transform.position = Owner.transform.position;
    }

    void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject ogo = other.gameObject;
        if (ogo == Owner)
        {
            return;
        }

        if ((ogo.layer & TargetsMask) != 0)
        {
            ogo.GetComponent<Entity>().TakeDamage(Damage);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position += (Vector3) Direction * (5f * Time.deltaTime);
    }
}