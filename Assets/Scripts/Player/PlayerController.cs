using System.Collections;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    public GameplayUI instance;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Boundaries")]
    [SerializeField] float boundaryPadding = .5f;

    [Header("Health")]
    [SerializeField] int maxHealth = 5;

    [SerializeField] string[] damageTag;

    Camera _cam;
    SpriteRenderer _sr;
    Vector3 _targetPos;
    int _health;
    float _camZAbs;
    Vector2 _minBound, _maxBound;
    static readonly WaitForSeconds _flashDelay = new(.3f);

    void Awake()
    {
        _cam = Camera.main;
        _sr = GetComponent<SpriteRenderer>();
        _camZAbs = Mathf.Abs(_cam.transform.position.z);

        var bl = _cam.ViewportToWorldPoint(new Vector3(0, 0, _camZAbs));
        var tr = _cam.ViewportToWorldPoint(new Vector3(1, 1, _camZAbs));
        _minBound = new Vector2(bl.x + boundaryPadding, bl.y + boundaryPadding);
        _maxBound = new Vector2(tr.x - boundaryPadding, tr.y - boundaryPadding);

        _health = maxHealth;
        _targetPos = transform.position;
    }
    private void Start()
    {
        instance = FindFirstObjectByType<GameplayUI>(); 
        GameManager.Instance.isGameOver = false;
        instance.ControlLife(_health);
    }
    void Update()
    {
        if (TryGetPointerPosition(out var pos)) _targetPos = pos;

        transform.position =
            Vector3.MoveTowards(transform.position, _targetPos, moveSpeed * Time.deltaTime);
    }

    bool TryGetPointerPosition(out Vector3 worldPos)
    {
        Vector3 screenPos;
        if (Input.touchCount > 0) screenPos = Input.GetTouch(0).position;
        else if (Input.GetMouseButton(0)) screenPos = Input.mousePosition;
        else { worldPos = default; return false; }

        worldPos = _cam.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, _camZAbs));
        worldPos.z = 0;
        worldPos.x = Mathf.Clamp(worldPos.x, _minBound.x, _maxBound.x);
        worldPos.y = Mathf.Clamp(worldPos.y, _minBound.y, _maxBound.y);
        return true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Coin"))
        {
            instance.ControlCoin(1);
            Destroy(col.gameObject);
            return;
        }

        foreach (var tag in damageTag)
        {
            if (col.CompareTag(tag))
            {
                Destroy(col.gameObject);
                ApplyDamage(1);
                break;
            }
        }
    }

    void ApplyDamage(int amount)
    {
        _health -= amount;
        instance.ControlLife(_health);
        if (_health <= 0)
        {
            GameManager.Instance.isGameOver = true;
            return;
        }
        StartCoroutine(FlashDamage());
    }

    IEnumerator FlashDamage()
    {
        _sr.color = Color.red;
        yield return _flashDelay;
        _sr.color = Color.white;
    }
}
