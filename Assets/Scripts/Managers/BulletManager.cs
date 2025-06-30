using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject explosionPrefab;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collision detected: {gameObject.tag}");
        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (collision.CompareTag("Player"))
            {
                Destroy(this.gameObject);
            }
        }

        if (gameObject.CompareTag("Projectile"))
        {
            if (collision.CompareTag("Enemy") || collision.CompareTag("Missile"))
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void Update()
    {
            Destroy(gameObject, 3f);
    }
    private void OnDestroy()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }
}
