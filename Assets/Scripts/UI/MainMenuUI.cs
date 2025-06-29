using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button playButton;
    public Button newGameButton;
    public Button shopButton;
    public Button optionsButton;
    public Button creditsButton;
    public Button exitButton;

    [Header("UI Elements")]
    public GameObject loadingPanel; // Opcional: painel de loading  
    public Text versionText; // Opcional: texto da vers�o  

    void Start()
    {        
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene("PlaneSelection"));

        newGameButton.onClick.RemoveAllListeners();
        newGameButton.onClick.AddListener(() => SceneManager.LoadScene("SaveLoad"));

        shopButton.onClick.RemoveAllListeners();
        shopButton.onClick.AddListener(() => SceneManager.LoadScene("Shop"));

        optionsButton.onClick.RemoveAllListeners();
        optionsButton.onClick.AddListener(() => SceneManager.LoadScene("Options"));

        creditsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.AddListener(() => SceneManager.LoadScene("Credits"));

        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(QuitGame);
    }

    private void InitializeMenu()
    {
        // Configurar vers�o do jogo (opcional)  
        if (versionText != null)
        {
            versionText.text = "v" + Application.version;
        }

        // Esconder painel de loading se existir  
        if (loadingPanel != null)
        {
            loadingPanel.SetActive(false);
        }

        // Garantir que o tempo est� normal  
        Time.timeScale = 1f;

        Debug.Log("MainMenu carregado com sucesso!");
    }

    private void LoadScene(string sceneName)
    {
        // Verificar se a cena existe  
        if (SceneExists(sceneName))
        {
            // Mostrar loading se dispon�vel  
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(true);
            }

            // Tocar som de clique  
            PlayClickSound();

            // Carregar cena  
            SceneManager.LoadScene(sceneName);

            Debug.Log($"Carregando cena: {sceneName}");
        }
        else
        {
            Debug.LogError($"Cena '{sceneName}' n�o encontrada! Verifique se est� adicionada no Build Settings.");

            // Tempor�rio: carregar Gameplay se a cena n�o existir  
            if (sceneName == "PlaneSelection")
            {
                SceneManager.LoadScene("Gameplay");
            }
        }
    }

    private void QuitGame()
    {
        // Tocar som de clique  
        PlayClickSound();

        Debug.Log("Saindo do jogo...");

        // Funciona no build, no editor s� para o play mode  
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();  
#endif
    }

    private bool SceneExists(string sceneName)
    {
        // Verificar se a cena existe no build settings  
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (name == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void PlayClickSound()
    {
        // Tocar som de clique se dispon�vel  
        if (AudioManager.Instance != null && AudioManager.Instance.coinSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coinSound);
        }
    }

    // M�todos p�blicos para serem chamados pelos bot�es (alternativa)  
    public void OnPlayButtonClicked()
    {
        LoadScene("PlaneSelection");
    }

    public void OnNewGameButtonClicked()
    {
        LoadScene("SaveLoad");
    }

    public void OnShopButtonClicked()
    {
        LoadScene("Shop");
    }

    public void OnOptionsButtonClicked()
    {
        LoadScene("Options");
    }

    public void OnCreditsButtonClicked()
    {
        LoadScene("Credits");
    }

    public void OnExitButtonClicked()
    {
        QuitGame();
    }
}
