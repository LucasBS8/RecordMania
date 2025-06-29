using UnityEngine;

public class ResetarProgressoAtual : MonoBehaviour
{
    public void ResetarProgresso()
    {
        PlayerPrefs.DeleteKey("PlayerCoins");
        PlayerPrefs.DeleteKey("PlayerLives");
        PlayerPrefs.DeleteKey("VidasDisponiveisParaCompra");
        PlayerPrefs.DeleteKey("PlaneBlue");
        PlayerPrefs.DeleteKey("PlaneRed");
        PlayerPrefs.Save();
        Debug.Log("Progresso do jogo atual resetado!");
    }
}
