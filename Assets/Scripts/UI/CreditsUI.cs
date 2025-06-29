using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    public void VoltarParaMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Use o nome exato da sua cena de menu principal
    }
}
