using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    void Start()
    {
        SpawnEnemies(2, 3);
    }

    void Update()
    {
        
    }

    void SpawnEnemies(int meleeCount, int rangeCount)
    {
        var enemies = new List<EnemyController>();
        
        for (var i = 0; i < meleeCount + rangeCount; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemy.transform.position =
                (Vector2) transform.position + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));

            var ec = enemy.GetComponent<EnemyController>();
            enemies.Add(ec);
        }

        for (var i = 0; i < meleeCount; i++)
        {
            enemies[i].Init(Weapon.Sword, enemies);
        }
        
        for (var i = 0; i < rangeCount; i++)
        {
            enemies[meleeCount + i].Init(Weapon.Bow, enemies);
        }
    }
}
