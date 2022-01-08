using UnityEngine;


[CreateAssetMenu(menuName = "Stats", fileName = "MyObjects")]
public class Stats : ScriptableObject
{
    public float health;
    public float damage;
    public float knockback;
    public float attackSpeed;
    public float movementSpeed;
    public float attackDistance;
}