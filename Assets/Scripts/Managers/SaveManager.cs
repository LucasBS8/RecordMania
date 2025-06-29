using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public SaveData[] saveSlots = new SaveData[5]; // 1 auto + 4 manual


    [Serializable]
    public class SaveData
    {
        public string saveDate;
        public int score;
        public int coins;
        public int level;
        public Dictionary<string, bool> unlockedItems = new Dictionary<string, bool>();
        public int totalCoins;
        public int highScore;
        public bool[] unlockedPlanes;
        public int selectedPlane;
    }

    private const string AUTO_SAVE_KEY = "autosave";
    private const int TOTAL_SAVE_SLOTS = 5;

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

    // Salva automaticamente no slot 1 (autosave)
    public void AutoSave()
    {
        SaveToSlot(0); // Slot 0 = autosave
    }

    // Salva em um slot específico (1-4 para slots manuais)
    public void SaveToSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= TOTAL_SAVE_SLOTS)
        {
            Debug.LogError($"Slot inválido: {slotIndex}. Escolha entre 0-{TOTAL_SAVE_SLOTS - 1}");
            return;
        }

        // Coletar dados do jogo
        SaveData saveData = new SaveData
        {
            saveDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
            score = GameManager.Instance.score,
            coins = GameManager.Instance.playerCoins,
            level = GameManager.Instance.currentLevel
            // Adicione outros dados do jogo aqui
        };

        // Salvar itens desbloqueados
        // Exemplo: saveData.unlockedItems["Item1"] = true;

        // Converter para JSON
        string json = JsonUtility.ToJson(saveData);

        // Gerar chave para o slot
        string saveKey = (slotIndex == 0) ? AUTO_SAVE_KEY : $"save_{slotIndex}";

        // Salvar usando PlayerPrefs
        PlayerPrefs.SetString(saveKey, json);
        PlayerPrefs.Save();

        Debug.Log($"Jogo salvo no slot {slotIndex}");
    }

    // Carrega de um slot específico
    public SaveData LoadFromSlot(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= TOTAL_SAVE_SLOTS)
        {
            Debug.LogError($"Slot inválido: {slotIndex}. Escolha entre 0-{TOTAL_SAVE_SLOTS - 1}");
            return null;
        }

        string saveKey = (slotIndex == 0) ? AUTO_SAVE_KEY : $"save_{slotIndex}";

        if (PlayerPrefs.HasKey(saveKey))
        {
            string json = PlayerPrefs.GetString(saveKey);
            return JsonUtility.FromJson<SaveData>(json);
        }

        return null; // Não há save neste slot
    }

    // Verifica se um slot tem dados salvos
    public bool SlotHasSave(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= TOTAL_SAVE_SLOTS)
            return false;

        string saveKey = (slotIndex == 0) ? AUTO_SAVE_KEY : $"save_{slotIndex}";
        return PlayerPrefs.HasKey(saveKey);
    }

    // Carrega um jogo e aplica os dados
    public void LoadGame(int slotIndex)
    {
        SaveData saveData = LoadFromSlot(slotIndex);

        if (saveData != null)
        {
            // Aplicar dados ao jogo
            GameManager.Instance.score = saveData.score;
            GameManager.Instance.playerCoins = saveData.coins;
            GameManager.Instance.currentLevel = saveData.level;

            // Carregar a cena de jogo
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
        else
        {
            Debug.LogWarning($"Nenhum save encontrado no slot {slotIndex}");
        }
    }

    // Inicia um novo jogo (limpa o progresso)
    public void StartNewGame(int slotIndex)
    {
        // Resetar dados
        GameManager.Instance.score = 0;
        GameManager.Instance.playerCoins = 100; // Valor inicial
        GameManager.Instance.currentLevel = 1;

        // Salvar imediatamente no slot escolhido
        SaveToSlot(slotIndex);

        // Carregar a cena de jogo
        UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
    }

    // Para debug
    public void DeleteAllSaves()
    {
        PlayerPrefs.DeleteKey(AUTO_SAVE_KEY);
        for (int i = 1; i < TOTAL_SAVE_SLOTS; i++)
        {
            PlayerPrefs.DeleteKey($"save_{i}");
        }
        PlayerPrefs.Save();
        Debug.Log("Todos os saves foram deletados!");
    }
}
