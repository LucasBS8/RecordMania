using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_MoedasTotal : MonoBehaviour
{
     private Banco MeuBanco;
     private TextMeshProUGUI meuTexto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MeuBanco = GameObject.FindGameObjectWithTag("GameController").GetComponent<Banco>();
        meuTexto = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        {
            meuTexto.text = MeuBanco.InformarValorNoBanco().ToString();
        }
    }
}
