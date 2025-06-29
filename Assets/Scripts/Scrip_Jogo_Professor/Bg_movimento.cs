using UnityEngine;

public class Bg_movimento : MonoBehaviour
{
    //variavel de velocidade
    public float velocidade_cenario;
    private MeshRenderer renderizador;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderizador = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float posy = Mathf.Repeat(Time.time * velocidade_cenario, 1);
        Vector2 offset = new Vector2(0, posy);
        renderizador.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
