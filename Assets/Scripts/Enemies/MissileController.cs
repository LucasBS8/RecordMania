using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    public GameObject explosionPrefab;

    void Start()
    {
        // Gera uma direção aleatória normalizada (qualquer direção 2D)
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    void Update()
    {
        // Move o míssel na direção definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Destroi o míssel se ele sair da tela
        if (!IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }

    // Verifica se o objeto está visível pela câmera
    private bool IsVisibleFrom(Camera cam)
    {
        var viewportPoint = cam.WorldToViewportPoint(transform.position);
        return viewportPoint.x > 0 && viewportPoint.x < 1 && viewportPoint.y > 0 && viewportPoint.y < 1;
    }
    private void OnDestroy()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
        Destroy(this.gameObject);
    }
}
