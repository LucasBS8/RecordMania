using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    public GameObject explosionPrefab;

    void Start()
    {
        // Gera uma dire��o aleat�ria normalizada (qualquer dire��o 2D)
        float angle = Random.Range(0f, 360f);
        float rad = angle * Mathf.Deg2Rad;
        direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }

    void Update()
    {
        // Move o m�ssel na dire��o definida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // Destroi o m�ssel se ele sair da tela
        if (!IsVisibleFrom(Camera.main))
        {
            Destroy(gameObject);
        }
    }

    // Verifica se o objeto est� vis�vel pela c�mera
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
