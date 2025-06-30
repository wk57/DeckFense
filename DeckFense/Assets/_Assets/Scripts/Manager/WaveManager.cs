using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] pathPoints; // El mismo arreglo que usas en EnemyController
    public Transform spawnPoint;   // Punto donde aparece el enemigo

    public int enemiesPerWave = 5;
    public float spawnDelay = 1f;

    public void StartWave()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, enemyPrefab.transform.rotation);
            EnemyController controller = enemy.GetComponent<EnemyController>();
            controller.pathPoints = pathPoints; // Asigna los waypoints
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
