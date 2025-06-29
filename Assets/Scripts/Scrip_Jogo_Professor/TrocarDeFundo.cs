using UnityEngine;
using UnityEngine.UI;

public class TrocaFundo : MonoBehaviour
{
    public Image fundo1;
    public Image fundo2;
    public float tempoEntreTrocas = 5f;
    public float tempoFade = 1f;

    private void Start()
    {
        fundo1.gameObject.SetActive(true);
        fundo2.gameObject.SetActive(true);

        fundo1.color = new Color(1, 1, 1, 1); // Totalmente visível
        fundo2.color = new Color(1, 1, 1, 0); // Totalmente invisível

        InvokeRepeating("AlternarFundos", tempoEntreTrocas, tempoEntreTrocas);
    }

    bool usandoFundo1 = true;

    void AlternarFundos()
    {
        StopAllCoroutines();
        StartCoroutine(FazerFade(usandoFundo1 ? fundo1 : fundo2, usandoFundo1 ? fundo2 : fundo1));
        usandoFundo1 = !usandoFundo1;
    }

    System.Collections.IEnumerator FazerFade(Image de, Image para)
    {
        float t = 0;
        while (t < tempoFade)
        {
            float alpha = t / tempoFade;
            de.color = new Color(1, 1, 1, 1 - alpha);
            para.color = new Color(1, 1, 1, alpha);
            t += Time.deltaTime;
            yield return null;
        }

        de.color = new Color(1, 1, 1, 0);
        para.color = new Color(1, 1, 1, 1);
    }
}