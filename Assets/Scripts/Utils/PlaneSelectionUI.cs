using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlaneSelectionUI : MonoBehaviour
{
    public Button aviaoPadraoButton;
    public Button aviaoAzulButton;
    public Button aviaoVermelhoButton;

    // Para guardar o avi�o escolhido
    public static string selectedPlane = "Padrao";

    void Start()
    {
        // Verifica se os bot�es foram atribu�dos no Inspector
        if (aviaoPadraoButton == null || aviaoAzulButton == null || aviaoVermelhoButton == null)
        {
            Debug.LogError("PlaneSelectionUI: Um ou mais bot�es n�o foram atribu�dos no Inspector!");
            return;
        }

        // Avi�o padr�o sempre dispon�vel
        aviaoPadraoButton.interactable = true;

        // Verifica se o PlaneUnlockManager existe
        if (PlaneUnlockManager.Instance != null)
        {
            // Verifica e configura o avi�o azul
            bool aviaoAzulDesbloqueado = PlaneUnlockManager.Instance.IsPlaneUnlocked("PlaneBlue");
            aviaoAzulButton.interactable = aviaoAzulDesbloqueado;
            SetButtonTransparency(aviaoAzulButton, aviaoAzulDesbloqueado);

            // Verifica e configura o avi�o vermelho
            bool aviaoVermelhoDesbloqueado = PlaneUnlockManager.Instance.IsPlaneUnlocked("PlaneRed");
            aviaoVermelhoButton.interactable = aviaoVermelhoDesbloqueado;
            SetButtonTransparency(aviaoVermelhoButton, aviaoVermelhoDesbloqueado);

            Debug.Log("Status dos avi�es: Azul: " + aviaoAzulDesbloqueado + ", Vermelho: " + aviaoVermelhoDesbloqueado);
        }
        else
        {
            // Se o PlaneUnlockManager n�o existir, desabilita os avi�es especiais
            Debug.LogWarning("PlaneUnlockManager n�o encontrado! Os avi�es especiais ser�o desabilitados.");

            aviaoAzulButton.interactable = false;
            SetButtonTransparency(aviaoAzulButton, false);

            aviaoVermelhoButton.interactable = false;
            SetButtonTransparency(aviaoVermelhoButton, false);
        }

        // Adiciona os listeners para os cliques nos bot�es
        aviaoPadraoButton.onClick.AddListener(() => SelecionarAviao("Padrao"));
        aviaoAzulButton.onClick.AddListener(() => SelecionarAviao("Azul"));
        aviaoVermelhoButton.onClick.AddListener(() => SelecionarAviao("Vermelho"));
    }

    void SetButtonTransparency(Button btn, bool enabled)
    {
        var colors = btn.colors;
        Color transparente = new Color(1, 1, 1, 0.3f);
        colors.normalColor = enabled ? Color.white : transparente;
        colors.highlightedColor = enabled ? Color.white : transparente;
        colors.pressedColor = enabled ? Color.white : transparente;
        btn.colors = colors;
    }

    void SelecionarAviao(string nome)
    {
        selectedPlane = nome;
        PlayerPrefs.SetString("SelectedPlane", nome); // Salva a escolha
        PlayerPrefs.Save();
        // Carrega a cena de gameplay
        SceneManager.LoadScene("Gameplay");
    }
}
