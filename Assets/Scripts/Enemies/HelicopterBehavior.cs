using UnityEngine;

public class HelicopterBehavior : MonoBehaviour
{
    public int health = 3; // Vida do helic�ptero
    public float entrySpeed = 3f;
    public float attackDuration = 5f;
    public float fireInterval = 1f;
    public float exitSpeed = 4f;
    public Vector2 attackPositionRange = new Vector2(2f, 4f); // Posi��o Y de ataque aleat�ria
    public GameObject bulletPrefab;
    public GameObject explosionPrefab; // Prefab de explos��o, se quiser usar
    public Transform firePoint;
    public Transform player;

    private enum State { Entering, Attacking, Exiting }
    private State currentState = State.Entering;

    private float attackTimer = 0f;
    private float fireTimer = 0f;
    private Vector3 targetAttackPosition;

    private Animator animator;

    void Start()
    {
        // Escolhe uma posi��o Y aleat�ria para a entrada
        float targetY = Random.Range(targetAttackPosition.x, targetAttackPosition.y);
        targetAttackPosition = new Vector3(transform.position.x, targetY, 0f);

        animator = GetComponent<Animator>();

    }

    void Update()
    {
        switch (currentState)
        {
            case State.Entering:
                MoveToAttackPosition();
                break;
            case State.Attacking:
                AttackPlayer();
                break;
            case State.Exiting:
                ExitScene();
                break;
        }
    }

    void MoveToAttackPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetAttackPosition, entrySpeed * Time.deltaTime);

        // Ativa anima��o de voo normal ou entrada suave
        if (animator != null)
        {
            animator.SetBool("IsFalling", true); // Exemplo: helic�ptero entra como se estivesse caindo um pouco
        }

        if (Vector3.Distance(transform.position, targetAttackPosition) < 0.1f)
        {
            currentState = State.Attacking;
            attackTimer = attackDuration;

            if (animator != null)
            {
                animator.SetBool("IsFalling", false); // Agora paira no ar
            }
        }
    }

    void AttackPlayer()
    {


        attackTimer -= Time.deltaTime;
        fireTimer -= Time.deltaTime;

        if (fireTimer <= 0f)
        {
            FireBullet();
            fireTimer = fireInterval;
        }

        if (attackTimer <= 0f)
        {
            currentState = State.Exiting;
        }
    }

    void ExitScene()
    {
        Vector3 exitTarget = new Vector3(transform.position.x > 0 ? 10f : -10f, transform.position.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, exitTarget, exitSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, exitTarget) < 0.5f)
        {
            Destroy(gameObject);
        }
    }

    void FireBullet()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (bulletPrefab != null && firePoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Vector2 direction = (player.position - firePoint.position).normalized;
            bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 6f; // Velocidade da bala
        }
    }

    public void Explode()
    {
        ScoreManager.Instance.AddPoint();
        CoinManager.Instance.DropCoin(transform.position);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Se colidir com um proj�til do player
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
            // Aqui voc� pode adicionar efeitos, anima��o, som, etc.
            if (health > 0)
            {
                health--; // Reduz a vida do helic�ptero
            }
            else
            {
                Explode(); // Se a vida chegar a zero, explode
            }
        }

        // Se colidir com o player (opcional)
        if (other.CompareTag("Player"))
        {
            // Aqui voc� pode causar dano ao player, game over, etc.
        }
    }

    // Se este script for redundante com HelicopterController, pode ser removido ou usado apenas para anima��es/efeitos.
    // Certifique-se de que o movimento, tiro e explos�o est�o centralizados em HelicopterController.
}
