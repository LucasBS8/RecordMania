using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Prefabs dos Aviões")]
    public GameObject playerPadraoPrefab;
    public GameObject playerAzulPrefab;
    public GameObject playerVermelhoPrefab;

    [Header("Configurações de Spawn")]
    public Vector3 spawnPosition = new(0f, -3f, 0f);

    public bool hasSpawned = false;

    void Start()
    {
        GameObject prefabToUse = GetCorrectPrefab();
        CreatePlayerInstance(prefabToUse);
    }

    private GameObject GetCorrectPrefab()
    {
        string selectedPlane = PlayerPrefs.GetString("SelectedPlane", "Padrao");

        GameObject targetPrefab = playerPadraoPrefab;

        switch (selectedPlane)
        {
            case "Azul":
                if (PlayerPrefs.GetInt("PlaneBlue", 0) == 1)
                {
                    targetPrefab = playerAzulPrefab;
                }
                break;

            case "Vermelho":
                if (PlayerPrefs.GetInt("PlaneRed", 0) == 1)
                {
                    targetPrefab = playerVermelhoPrefab;
                }
                break;

            default:
                targetPrefab = playerPadraoPrefab;
                break;
        }
        return targetPrefab;
    }

    private void CreatePlayerInstance(GameObject prefab)
    {
        Debug.Log($"Instanciando o prefab: {prefab.name} na posição: {spawnPosition}");
        Instantiate(prefab, spawnPosition, Quaternion.identity);
        Debug.Log("Instância do jogador criada com sucesso.");
        hasSpawned = true;
    }
}
