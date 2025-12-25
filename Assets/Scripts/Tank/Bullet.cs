using UnityEngine;

// ÃÑ¾Ë ½ºÅ©¸³Æ®
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;   // ÃÑ¾Ë ¼Óµµ
    public int damage;                            // ÃÑ¾Ë µ¥¹ÌÁö

    public Transform target;                      // ¸ñÇ¥ Å¸°Ù

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb.linearVelocity = transform.up * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target != null && collision.transform != target) return;

        Destroy(gameObject);
    }
}
