using UnityEngine;

public class PlaneUnlockManager : MonoBehaviour
{
    public static PlaneUnlockManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Salva desbloqueio do avião
    public void UnlockPlane(string planeKey)
    {
        PlayerPrefs.SetInt(planeKey, 1);
        PlayerPrefs.Save();
    }

    // Verifica se avião está desbloqueado
    public bool IsPlaneUnlocked(string planeKey)
    {
        return PlayerPrefs.GetInt(planeKey, 0) == 1;
    }
}
