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
        Debug.Log("A��o confirmada!");
        // Aqui voc� faz a a��o desejada, ex: Application.Quit();
        EsconderPainel();
    }
}
