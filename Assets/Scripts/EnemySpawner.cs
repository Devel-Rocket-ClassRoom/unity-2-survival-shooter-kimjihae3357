using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] enemyPrefabs;
    public Transform[] spawnPoints;

    public int maxEnemyCount = 5;

    private List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (enemies.Count < maxEnemyCount)
            {
                Spawn();
            }
            yield return new WaitForSeconds(2f);
        }
       
    }

    private void Spawn()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Enemy enemy = Instantiate(enemyPrefabs[enemyIndex], point.position, point.rotation);

        enemy.spawner = this;

        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }
}
