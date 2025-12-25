using System.Collections;
using UnityEngine;

// 적 생성 스크립트
public class EnemySpawner : MonoBehaviour
{
    [Header("# Enemy Object")]
    public GameObject enemyPrefab;                          // 적 오브젝트

    [Header("# Spawner Settings")]
    [SerializeField] private Transform spawnPoint;          // 적 생성 위치
    [SerializeField] private int baseEnemyPerWaves;         // 기본 생성될 적의 수
    [SerializeField] private float timeBetweenWaves;        // 다음 웨이브 시작되기까지의 대기 시간
    [SerializeField] private float enemySpawnInterval;      // 적 생성 간격
    [SerializeField] private int aliveEnemies;              // 살아있는 적

    private int currentWaveIndex = 1;                       // 현재 웨이브

    private void Start()
    {
        StartCoroutine(WaveLoop());
    }
    private IEnumerator WaveLoop()
    {
        while(true)
        {
            Debug.Log($"Wave {currentWaveIndex}단계 시작");
            yield return SpawnWave();
            
            yield return new WaitUntil(() => aliveEnemies == 0);

            Debug.Log($"Wave {currentWaveIndex}단계 종료");

            currentWaveIndex++;
            Debug.Log($"Wave {currentWaveIndex}단계 준비중");

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
    private IEnumerator SpawnWave()
    {
        int enemyCount = baseEnemyPerWaves + (currentWaveIndex - 1) * 2;
        aliveEnemies = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(enemySpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

        EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.OnArrive += Arrive;
        }
    }
    private void OnEnable()
    {
        Enemy.OnDie += OneEnemyDie;
    }

    // 적이 죽거나 마지막 wayPoint에 도달하면 aliveEnemy를 감소시킴
    private void Arrive(EnemyMovement enemyMovement)
    {
        aliveEnemies--;
    }
    private void OneEnemyDie()
    {
        aliveEnemies--;
    }
}
