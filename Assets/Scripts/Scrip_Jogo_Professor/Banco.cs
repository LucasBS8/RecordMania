using UnityEngine;

public class Banco : MonoBehaviour
{
    private int valorBanco;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GuardarNoBanco(int novoValor)
    {
        //Recebo o que ja tenho no banco
        valorBanco = PlayerPrefs.GetInt("minhasMoedas");
        //Somo com o novo valor
        valorBanco = valorBanco + novoValor;
        //Guardo o valor total
        PlayerPrefs.SetInt("minhasMoedas", valorBanco);
    }

    //Informa a quantidade de dinheiro que possuo
    public int InformarValorNoBanco()
    {
        //Retorno o valor que tenho no banco
        valorBanco = PlayerPrefs.GetInt("minhasMoedas");
        return valorBanco;
    }

    /// <summary>
    /// Retirada
    /// </summary>

    public void RetirardooBanco(int novoValor)
    {
        //Recebo o que ja tenho no banco
        valorBanco = PlayerPrefs.GetInt("minhasMoedas");
        //Subtrair o novo valor
        valorBanco = valorBanco - novoValor;
        //Guardo o valor total
        PlayerPrefs.SetInt("minhasMoedas", valorBanco);
    }

    public bool Pagar(int custo)
    {
        int dinheiroBanco = InformarValorNoBanco();
        if (dinheiroBanco >= custo)
        {
            //Pude pagar e paguei
            RetirardooBanco(custo);
            return true;
        }
        else
        {
            //Não pude pagar
            return false;
        }
    }
}
