using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score = 0;
    public int highScore = 0;

    public TextMeshProUGUI scoreText;     // Arraste o texto do score atual
    public TextMeshProUGUI highScoreText; // Arraste o texto do recorde

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Carrega o recorde salvo
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoint()
    {
        score++;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        // Salva o recorde se o score atual for maior
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Recorde: " + score;
        if (highScoreText != null)
            highScoreText.text = "Maior Recorde: " + highScore;
    }
}
