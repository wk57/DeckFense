using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class WaveManager : MonoBehaviour
{
    public Transform[] pathPoints;
    public List<EnemyData> enemyTypes = new List<EnemyData>();
    public float spawnInterval = 1f;
    private int enemiesAlive = 0;
    public GameObject enemyPrefab;   


    public void StartWave(int enemyCount)
    {
        StartCoroutine(SpawnEnemies(enemyCount));
    }
   

    private IEnumerator SpawnEnemies(int count)
    {
        enemiesAlive = count;

        for (int i = 0; i < count; i++)
        {
            EnemyData selectedEnemyData = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];

            GameObject newEnemy = Instantiate(selectedEnemyData.enemyPrefab, pathPoints[0].position, Quaternion.identity);
            EnemyController ec = newEnemy.GetComponent<EnemyController>();

            // Asigna sus valores desde EnemyData
            ec.data = selectedEnemyData;
            ec.pathPoints = pathPoints;
            ec.speed = selectedEnemyData.speed;
            ec.maxHealth = selectedEnemyData.health;
            ec.onDeath += OnEnemyDeath;

            yield return new WaitForSeconds(1f); // Delay entre enemigos
        }
    }

    private void OnEnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive <= 0)
        {
            GameManager.Instance.currentWave++;
            GameManager.Instance.StartNextWave();
        }
    }


}
