using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab;
    public float minSpawnInterval = 0.5f;
    public float maxSpawnInterval = 2f;
    public float spawnRadius = 10f;

    private float nextSpawnTime;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnMissile();
            ScheduleNextSpawn();
        }
    }

    void ScheduleNextSpawn()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
    }

    void SpawnMissile()
    {
        // Posi��o aleat�ria na borda de um c�rculo ao redor do centro da tela
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        Vector3 spawnPos = Camera.main.transform.position + (Vector3)(randomDir * spawnRadius);
        spawnPos.z = 0; // Garante que est� no plano 2D

        Instantiate(missilePrefab, spawnPos, Quaternion.identity);
    }
}
