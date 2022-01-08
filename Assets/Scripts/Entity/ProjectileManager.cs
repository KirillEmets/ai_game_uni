using UnityEngine;


public class ProjectileManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    
    public void CreateArrow(GameObject owner, float damage, Vector2 direction, int targetsMask)
    {
        var arrow = Instantiate(arrowPrefab);
        arrow.GetComponent<ArrowScript>().Init(owner, damage, direction, targetsMask);
    }
}