using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    public static GameplayUI Instance;

    [Header("UI Elements")]
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI livesText;
    public GameObject gameOverPanel;
    public GameObject hud;

    private int playerCoins;
    private int playerLives;

/*    void Awake()
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
*/
    void Start()
    {
        LoadPlayerData();
        UpdateUI();
    }

    private void LoadPlayerData()
    {
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 0);
        playerLives = PlayerPrefs.GetInt("PlayerLives", 5);
    }

    public void UpdateUI()
    {
        coinsText.text = playerCoins.ToString();
        livesText.text = playerLives.ToString();
    }

    public void ControlCoin(int amount)
    {
        playerCoins += amount;
        PlayerPrefs.SetInt("PlayerCoins", playerCoins);
        PlayerPrefs.Save();
        UpdateUI();
    }

    public void ControlLife(int life)
    {
        playerLives = life;
        UpdateUI();
        if (playerLives <= 0)
        {
            ShowGameOverPanel();
        }
    }

    public void ShowGameOverPanel()
    {
        hud.SetActive(false);
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReturnMenu()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        hud.SetActive(true);
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        gameOverPanel.SetActive(false);
        hud.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

