using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ShopUI : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI vidasDisponiveisText;
    public TextMeshProUGUI livesText;

    public int playerCoins = 0;
    public int vidasDisponiveisParaCompra = 15;
    public int playerLives = 5;
    public int maxLives = 20;


    [System.Serializable]
    public class ShopItem
    {
        public string itemName;
        public int price;
        public Button buyButton;
        public TextMeshProUGUI priceText;
        public TextMeshProUGUI quantityLabel; // arraste o texto "1x" do avi�o
    }

    public ShopItem[] items;
    private object currentItem;

    void Start()
    {
        // Carregar dados persistentes  
        playerLives = PlayerPrefs.GetInt("PlayerLives", 5);
        vidasDisponiveisParaCompra = PlayerPrefs.GetInt("VidasDisponiveisParaCompra", 15);
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", 300);

        UpdateCoinsUI();
        UpdateLivesUI();
        UpdateVidasDisponiveisUI();

        foreach (var item in items)
        {
            if (item.itemName == "Vida Extra")
            {
                item.price = 50 * (playerLives - 4);
                item.priceText.text = item.price.ToString();

                // S� desativa se realmente n�o puder comprar mais (vidas m�ximas ou vidas dispon�veis esgotadas)  
                item.buyButton.interactable = (playerLives < maxLives && vidasDisponiveisParaCompra > 0);
            }
            else if (item.itemName == "Avi�o Azul" || item.itemName == "Avi�o Vermelho")
            {
                string planeKey = item.itemName == "Avi�o Azul" ? "PlaneBlue" : "PlaneRed";
                bool comprado = PlaneUnlockManager.Instance.IsPlaneUnlocked(planeKey);

                // Atualiza o label de quantidade  
                if (item.quantityLabel != null)
                    item.quantityLabel.text = comprado ? "0x" : "1x";

                // Torna o bot�o transparente se j� comprado, opaco se n�o  
                var colors = item.buyButton.colors;
                Color transparente = new Color(1, 1, 1, 0.3f);
                colors.normalColor = comprado ? transparente : Color.white;
                colors.highlightedColor = comprado ? transparente : Color.white;
                colors.pressedColor = comprado ? transparente : Color.white;
                item.buyButton.colors = colors;

                // S� desativa se j� estiver comprado  
                item.buyButton.interactable = !comprado;
                item.buyButton.gameObject.SetActive(true);
            }

            // Corrigir o problema de 'currentItem' sendo tratado como 'object'  
            item.buyButton.onClick.RemoveAllListeners();
            item.buyButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
            livesText.text = playerLives.ToString();
    }

    void BuyItem(ShopItem item)
    {
        if (item.itemName == "Vida Extra")
        {
            Debug.Log("Tentando comprar: " + item.itemName);
            if (playerLives >= maxLives)
            {
                Debug.Log("Voc� j� atingiu o m�ximo de vidas!");
                item.buyButton.interactable = false;
                return;
            }
            if (vidasDisponiveisParaCompra <= 0)
            {
                Debug.Log("N�o h� mais vidas dispon�veis para compra!");
                item.buyButton.interactable = false;
                return;
            }
            if (playerCoins >= item.price)
            {
                playerCoins -= item.price;
                playerLives++;
                vidasDisponiveisParaCompra--;

                PlayerPrefs.SetInt("PlayerLives", playerLives);
                PlayerPrefs.SetInt("VidasDisponiveisParaCompra", vidasDisponiveisParaCompra);
                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                PlayerPrefs.Save();

                item.price = 50 * (playerLives - 4);
                item.priceText.text = item.price.ToString();

                UpdateCoinsUI();
                UpdateLivesUI();
                UpdateVidasDisponiveisUI();

                // S� desativa se n�o puder comprar mais
                if (playerLives >= maxLives || vidasDisponiveisParaCompra <= 0)
                    item.buyButton.interactable = false;
                else
                    item.buyButton.interactable = true;

                Debug.Log("Comprou vida extra! Vidas: " + playerLives);
            }
            else
            {
                Debug.Log("Moedas insuficientes!");
            }

        }
        else if (item.itemName == "Avi�o Azul" || item.itemName == "Avi�o Vermelho")
        {
            string planeKey = item.itemName == "Avi�o Azul" ? "PlaneBlue" : "PlaneRed";
            if (PlaneUnlockManager.Instance.IsPlaneUnlocked(planeKey))
            {
                Debug.Log("Este avi�o j� foi comprado!");
                item.buyButton.interactable = false;
                return;
            }
            if (playerCoins >= item.price)
            {
                playerCoins -= item.price;
                PlaneUnlockManager.Instance.UnlockPlane(planeKey);

                PlayerPrefs.SetInt("PlayerCoins", playerCoins);
                PlayerPrefs.Save();

                UpdateCoinsUI();

                // Atualiza o label para "0x"
                if (item.quantityLabel != null)
                    item.quantityLabel.text = "0x";

                // Torna o bot�o transparente e n�o-interativo
                var colors = item.buyButton.colors;
                Color transparente = new Color(1, 1, 1, 0.3f);
                colors.normalColor = transparente;
                colors.highlightedColor = transparente;
                colors.pressedColor = transparente;
                item.buyButton.colors = colors;
                item.buyButton.interactable = false;

                Debug.Log($"Comprou: {item.itemName}");
            }
            else
            {
                Debug.Log("Moedas insuficientes!");
            }
        }
        else
        {
            // L�gica para outros itens
        }
    }

    void UpdateCoinsUI()
    {
        if (coinsText != null)
            coinsText.text = playerCoins.ToString();
    }

    void UpdateVidasDisponiveisUI()
    {
        if (vidasDisponiveisText != null)
            vidasDisponiveisText.text = vidasDisponiveisParaCompra.ToString();
    }

    public void VoltarParaMenu()
    {
        if (AudioManager.Instance != null && AudioManager.Instance.coinSound != null)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.coinSound);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
