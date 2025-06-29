using UnityEngine;

public class Aviao_inimigo_Script : MonoBehaviour
{
    public int hp_aviao_inimigo = 2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PerdeHP(int dano)
    {
        hp_aviao_inimigo = hp_aviao_inimigo - dano;
        if (hp_aviao_inimigo <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
