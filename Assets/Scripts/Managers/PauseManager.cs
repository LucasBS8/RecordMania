using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;

    [Header("UI Elements")]
    public GameObject pausePanel; // Arraste o painel de pause no Inspector

    private bool isPaused = false;
    private GameObject currentPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("PauseManager Instance criada");
        }
        else
        {
            Debug.Log("PauseManager duplicada encontrada, destruindo...");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializePauseManager();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void InitializePauseManager()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        // Encontrar o player dinamicamente
        FindCurrentPlayer();

        Debug.Log("PauseManager inicializado");
    }

    private void FindCurrentPlayer()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        if (currentPlayer != null)
        {
            Debug.Log("Player encontrado: " + currentPlayer.name);
        }
        else
        {
            Debug.LogWarning("Player não encontrado!");
        }
    }

    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;

        Debug.Log("Jogo pausado");
    }

    public void ResumeGame()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;

        Debug.Log("Jogo retomado");
    }

    public void GoToMainMenu()
    {
        Debug.Log("Voltando ao menu principal...");

        // Garantir que o tempo volte ao normal
        Time.timeScale = 1f;
        isPaused = false;

        // Limpar referências antes de trocar de cena
        currentPlayer = null;

        // Carregar a cena do menu principal
        SceneManager.LoadScene("MainMenu");
    }
}

