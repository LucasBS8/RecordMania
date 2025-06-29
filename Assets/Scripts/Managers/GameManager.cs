using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool isGamePaused = false;
    public bool isGameOver = false;

    [Header("Game Stats")]
    public int currentScore = 0;
    public int coinsEarned = 0;
    public int score;
    public int playerCoins = 0;
    public int currentLevel = 1;
    internal int playerLives;

    void Start()
    {
        InvokeRepeating(nameof(AutoSaveGame), 2f, 120f);
    }

    void Awake()
    {
        Time.timeScale = 1f;
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

    private void AutoSaveGame()
    {
        if (!isGamePaused && !isGameOver)
        {
            SaveManager.Instance.AutoSave();
            Debug.Log("Jogo salvo automaticamente!");
        }
    }
}
