using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [Header("Coin Prefab")]
    public GameObject coinPrefab;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DropCoin(Vector3 position)
    {
        Instantiate(coinPrefab, position, Quaternion.identity);
    }
}
