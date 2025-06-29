using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;

    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f;
    private float contador;

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= fireRate && Input.GetButton("Fire1"))
        {
            contador = 0f;
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = Vector2.up * bulletSpeed;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shootSound);
    }
}
