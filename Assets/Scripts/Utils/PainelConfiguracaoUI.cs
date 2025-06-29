using UnityEngine;

public class PainelConfirmacaoUI : MonoBehaviour
{
    public GameObject painelConfirmacao;

    public void MostrarPainel()
    {
        painelConfirmacao.SetActive(true);
    }

    public void EsconderPainel()
    {
        painelConfirmacao.SetActive(false);
    }

    public void ConfirmarAcao()
    {
        Debug.Log("Ação confirmada!");
        // Aqui você faz a ação desejada, ex: Application.Quit();
        EsconderPainel();
    }
}
