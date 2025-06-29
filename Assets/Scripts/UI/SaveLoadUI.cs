using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class SaveLoadUI : MonoBehaviour
{
    [System.Serializable]
    public class SaveSlotUI
    {
        public Button slotButton;
        public TextMeshProUGUI slotLabel;
        public TextMeshProUGUI saveInfo;
        //public TextMeshProUGUI slotImage;
    }

    public SaveSlotUI[] saveSlots; // Array de 5 slots na interface
    public GameObject confirmationPanel;
    public TextMeshProUGUI confirmationText;
    private int selectedSlot = -1;

    void Start()
    {
        // Esconder painel de confirmação
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);

        // Configurar UI dos slots
        RefreshSlotsUI();

        // Configurar eventos dos botões
        for (int i = 0; i < saveSlots.Length; i++)
        {
            int slotIndex = i; // Importante criar uma cópia para o closure
            saveSlots[i].slotButton.onClick.AddListener(() => OnSlotButtonClicked(slotIndex));
        }
    }

    void RefreshSlotsUI()
    {
        for (int i = 0; i < saveSlots.Length; i++)
        {
            bool hasSave = SaveManager.Instance.SlotHasSave(i);
            SaveManager.SaveData saveData = null;

            if (hasSave)
            {
                saveData = SaveManager.Instance.LoadFromSlot(i);

                // Atualizar informações do save
                if (i == 0)
                    saveSlots[i].slotLabel.text = "AutoSave";
                else
                    saveSlots[i].slotLabel.text = $"Slot {i}";

                saveSlots[i].saveInfo.text = $"{saveData.saveDate}\nNível: {saveData.level}\nMoedas: {saveData.coins}";
            }
            else
            {
                // Slot vazio
                if (i == 0)
                    saveSlots[i].slotLabel.text = "AutoSave (vazio)";
                else
                    saveSlots[i].slotLabel.text = $"Slot {i} (vazio)";

                saveSlots[i].saveInfo.text = "Nenhum save neste slot";
            }
        }
    }

    void OnSlotButtonClicked(int slotIndex)
    {
        selectedSlot = slotIndex;
        bool hasSave = SaveManager.Instance.SlotHasSave(slotIndex);

        if (hasSave)
        {
            // Mostrar confirmação
            confirmationText.text = $"O que deseja fazer com o Slot {slotIndex}?\n- Carregar\n- Sobrescrever\n- Resetar Slot";
            confirmationPanel.SetActive(true);
        }
        else
        {
            // Slot vazio - criar novo jogo
            SaveManager.Instance.StartNewGame(slotIndex);
        }
    }

    public void OnLoadButtonClicked()
    {
        if (selectedSlot >= 0)
        {
            SaveManager.Instance.LoadGame(selectedSlot);
            confirmationPanel.SetActive(false);
        }
    }

    public void OnOverwriteButtonClicked()
    {
        if (selectedSlot >= 0)
        {
            // Inicia um novo jogo no slot selecionado
            SaveManager.Instance.StartNewGame(selectedSlot);
            confirmationPanel.SetActive(false);
        }
    }

    public void OnCancelButtonClicked()
    {
        confirmationPanel.SetActive(false);
        selectedSlot = -1;
    }

    public void OnBackToMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnResetSlotButtonClicked()
    {
        if (selectedSlot >= 0)
        {
            // Reseta o slot e inicia um novo jogo nesse slot
            SaveManager.Instance.StartNewGame(selectedSlot);
            confirmationPanel.SetActive(false);
        }
    }
}
