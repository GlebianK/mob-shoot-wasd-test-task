using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;

    [Tooltip("»ндекс элемента равен соответствующему индексу списка enemyPrefabs. ”бедитесь, что они одногоразмера и расположены в верном пор€дке")]
    [SerializeField] private List<int> numOfEnemies;

    [SerializeField] private int spawnCooldownMinTimer = 3;
    [SerializeField] private int spawnCooldownMaxTimer = 7;

    private bool canSpawn;
    private Queue<GameObject> enemiesPool;
    private int totalEnemies;

    private void Awake()
    {
        canSpawn = false;

        if (enemyPrefabs.Count != numOfEnemies.Count)
            throw new System.Exception("—писки врагов и их количества не совпадают по размеру!");

        if (enemyPrefabs.Count < 1 || numOfEnemies.Count < 1)
            throw new System.Exception(" акой-то из списков не заполнен!");

        if (enemyPrefabs == null || numOfEnemies == null)
            throw new System.ArgumentNullException(" акой-то из списков null!");

        enemiesPool = new();
        canSpawn = true;
    }

    private void Start()
    {
        InitializeEnemiesPool();
        StartCoroutine(SpawnEnemy());
    }

    private void OnDisable()
    {
        canSpawn = false;
        StopAllCoroutines();
    }

    private void InitializeEnemiesPool()
    {
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for(int j = 0; j < numOfEnemies.Count; j++)
            {
                GameObject enemy = Instantiate(enemyPrefabs[i], transform.position, Quaternion.identity);
                enemy.SetActive(false);
                enemy.transform.parent = null;
                enemiesPool.Enqueue(enemy);

                if (enemy.TryGetComponent<EnemyBehavior>(out EnemyBehavior ebComponent))
                    ebComponent.SetParentPool(gameObject);
                else
                    throw new System.Exception("EnemySpawner -> No EnemyBehavior components found!");
            }
        }
        totalEnemies = enemiesPool.Count;
        GameManager.Instance.AddEnemyAmount(totalEnemies);
    }

    public void ReturnEnemyToPool(GameObject enemyToReturn)
    {
        enemiesPool.Enqueue(enemyToReturn);
        enemyToReturn.SetActive(false);
        enemyToReturn.transform.position = transform.position;
    }

    private IEnumerator SpawnEnemy()
    {
        yield return null;

        while (canSpawn)
        {
            if (enemiesPool.Count < 1)
            {
                yield return null;
                continue;
            }

            GameObject newEnemy = enemiesPool.Dequeue();
            newEnemy.SetActive(true);

            float temp = Random.Range(spawnCooldownMinTimer, spawnCooldownMaxTimer);
            yield return new WaitForSeconds(temp);
        }
        yield return null;
    }
}
