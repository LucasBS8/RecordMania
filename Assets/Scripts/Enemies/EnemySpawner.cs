using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs; // 5 tipos diferentes

    [Header("Spawn Settings")]
    public float spawnRate = 1f;
    public float spawnRateIncrease = 0.1f;
    public int maxEnemiesOnScreen = 8;

    [Header("Spawn Boundaries")]
    public float spawnHeight = 6f; // Altura onde inimigos aparecem
    public float spawnRangeX = 8f; // Largura da �rea de spawn

    [Header("Missile System")]
    public GameObject missilePrefab;
    public float missileInterval = 30f;
    private float nextMissileTime;

    [Header("Difficulty")]
    public float difficultyIncreaseTime = 10f; // A cada 10 segundos
    private float lastDifficultyIncrease;

    public List<GameObject> activeEnemies = new List<GameObject>();
    public float nextSpawnTime;
    private Camera mainCamera;
    public float contador;

    void Start()
    {
        mainCamera = Camera.main;
        nextSpawnTime = Time.deltaTime + spawnRate;
        nextMissileTime = Time.time + missileInterval;
        lastDifficultyIncrease = Time.time;
        if (enemyPrefabs.Length == 0)
        {
            Debug.LogError("EnemySpawner: Nenhum prefab de inimigo configurado!");
        }
    }

    void Update()
    {
        contador += Time.deltaTime;
        SpawnEnemies();
        if (!GameManager.Instance.isGamePaused && !GameManager.Instance.isGameOver)
        {
            CleanDestroyedEnemies();
            CheckMissiles();
            IncreaseDifficulty();
        }
    }

    private void SpawnEnemies()
    {
        if (contador >= nextSpawnTime)
        {
            if (enemyPrefabs.Length > 0)
            {
                // Escolher inimigo aleat�rio
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                GameObject enemyPrefab = enemyPrefabs[randomIndex];

                // Posi��o aleat�ria no topo da tela
                Vector3 spawnPosition = new Vector3(
                    Random.Range(-spawnRangeX, spawnRangeX),
                    spawnHeight,
                    0f
                );

                // Criar inimigo
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                activeEnemies.Add(enemy);

                // Pr�ximo spawn
                nextSpawnTime = contador + spawnRate;
            }
        }
    }

    private void CheckMissiles()
    {
        if (Time.time >= nextMissileTime && missilePrefab != null)
        {
            Vector3 missilePosition = new Vector3(
                Random.Range(-spawnRangeX, spawnRangeX),
                spawnHeight + 1f,
                0f
            );

            GameObject missile = Instantiate(missilePrefab, missilePosition, Quaternion.identity);
            activeEnemies.Add(missile);
            nextMissileTime = Time.time + missileInterval;

            if (AudioManager.Instance != null && AudioManager.Instance.explosionSound != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.explosionSound);
            }
        }
    }

    private void IncreaseDifficulty()
    {
        if (Time.time >= lastDifficultyIncrease + difficultyIncreaseTime)
        {
            spawnRate = Mathf.Max(0.3f, spawnRate - spawnRateIncrease);
            maxEnemiesOnScreen = Mathf.Min(15, maxEnemiesOnScreen + 1);
            lastDifficultyIncrease = Time.time;
        }
    }

    private void CleanDestroyedEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            if (activeEnemies[i] == null)
            {
                activeEnemies.RemoveAt(i);
            }
        }
    }
}