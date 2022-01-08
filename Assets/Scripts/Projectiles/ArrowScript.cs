using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    private Vector2 Direction { get; set; } = Vector2.zero;
    private Collider2D Collider { get; set; }
    private GameObject Owner { get; set; }
    private ContactFilter2D Filter { get; set; }
    private float Damage { get; set; }

    public void Init(GameObject owner, float damage, Vector2 direction, int targetsMask)
    {
        Owner = owner;
        Direction = direction.normalized;

        var rotZ = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);


        // TargetsMask = targetsMask;
        Filter = new ContactFilter2D {useLayerMask = true, layerMask = targetsMask};
        Debug.Log(targetsMask);

        Damage = damage;

        transform.position = Owner.transform.position;
    }

    void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    readonly List<Collider2D> result = new List<Collider2D>();

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Debug.Log("kek collidsuo");
    //     var ogo = other.gameObject;
    //     if (ogo == Owner) return;
    //
    //
    //     if ((ogo.layer & TargetsMask) != 0)
    //     {
    //         ogo.GetComponent<Entity>().TakeDamage(Damage);
    //         Destroy(gameObject);
    //     }
    // }

    private void FixedUpdate()
    {
        transform.position += (Vector3) Direction * (8f * Time.deltaTime);

        result.Clear();

        if (Physics2D.OverlapCollider(Collider, Filter, result) <= 0) return;

        result[0].GetComponent<Entity>()?.TakeDamage(Damage);
        Destroy(gameObject);
    }

    IEnumerator KillTimer()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}