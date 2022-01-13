using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject dropItemPrefab;
    public GameObject spawnDots;
    private Transform[] dots;
    
    private int currentWave = 0;

    private (int, int)[][] Waves { get; } =
    {
        // new[] {(1, 0)},
        // new[] {(1, 1)},
        // new[] {(2, 1)},
        // new[] {(2, 2)},
        // new[] {(3, 0)},
        // new[] {(0, 3)},
        // new[] {(1, 0)},
        new[] {(1, 1), (1, 1)},
        new[] {(3, 0), (0, 3)},
        new[] {(2, 2), (2, 2), (2, 2)},
        new[] {(2, 2), (2, 2)},
        new[] {(2, 2), (2, 2)},
    };

    void Start()
    {
        dots = spawnDots.GetComponentsInChildren<Transform>();
        CreateEnemies(Waves[0]);
    }

    void CreateEnemies((int, int)[] groups)
    {
        var waveSize = groups.Sum(x => x.Item1 + x.Item2);
        var deadEnemies = 0;

        foreach (var group in groups)
        {
            var (m, r) = group;
            var i = Random.Range(0, dots.Length);
            var enemies = SpawnEnemies(dots[i].position, m, r);
            enemies.ForEach(e => e.OnDeathEvent += () =>
            {
                var go = Instantiate(dropItemPrefab);
                go.transform.position = e.transform.position;
                var drop = go.GetComponent<DropItemScript>();
                switch (e.Weapon)
                {
                    case Weapon.Sword:
                        drop.Init(DropItemType.Health, new HealthDropItemBehaviour());
                        break;
                    case Weapon.Bow:
                        drop.Init(DropItemType.Arrows, new ArrowDropItemBehaviour());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                deadEnemies += 1;
                if (deadEnemies != waveSize) return;

                currentWave += 1;
                var nextCount = currentWave < Waves.Length ? Waves[currentWave] : Waves.Last();
                StartCoroutine(nameof(WaitAndCreateNew), nextCount);
            });
        }
    }

    IEnumerator WaitAndCreateNew((int, int)[] count)
    {
        yield return new WaitForSeconds(6);
        CreateEnemies(count);
    }

    List<EnemyController> SpawnEnemies(Vector2 pos, int meleeCount, int rangeCount)
    {
        var enemies = new List<EnemyController>();

        for (var i = 0; i < meleeCount + rangeCount; i++)
        {
            var enemy = Instantiate(enemyPrefab);
            enemy.transform.position =
                pos + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));

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

        return enemies;
    }
}