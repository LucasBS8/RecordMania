using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;
    private Camera mainCamera;
    private float screenTop, screenBottom, screenLeft, screenRight;
    public GameObject explosionPrefab;

    void Start()
    {
        // Obtém a referência da câmera principal
        mainCamera = Camera.main;
        // Calcula todos os limites da tela em coordenadas mundiais
        Vector3 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        screenTop = screenBounds.y + 1f;    // Margem extra para o topo
        screenBottom = -screenBounds.y - 1f; // Margem extra para baixo
        screenLeft = -screenBounds.x - 1f;   // Margem extra para esquerda
        screenRight = screenBounds.x + 1f;   // Margem extra para direita

    }

    void Update()
    {
        // Move o tiro para cima
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        // Verifica se o tiro saiu da tela em qualquer direção
        Vector3 pos = transform.position;
        if (pos.y > screenTop || pos.y < screenBottom || pos.x < screenLeft || pos.x > screenRight)
        {
            Debug.Log($"Tiro destruído por sair da tela: {pos}");
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Enemy") || other.CompareTag("Missile"))
        {
            Destroy(other.gameObject);
        }
    }
    private void OnDestroy()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }
    public void SetDirection(Vector3 direction)
    {
        enabled = false;
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
    }
}
