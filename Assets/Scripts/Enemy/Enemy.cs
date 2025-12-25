using System;
using UnityEngine;

// 적 정보 스크립트
public class Enemy : MonoBehaviour
{
    [SerializeField] private int hp;          // 적 체력
    [SerializeField] private float moveSpeed; // 적 이동 속도
    [SerializeField] private int reawrd;

    private Rigidbody2D rb;
    private EnemyMovement _enemyMovement;

    // 적 죽음 확인 여부
    private bool isDead = false;

    // 적 죽음 이벤트 함수
    public static event Action OnDie;

    public static event Action<int> OnDropCoin;

    public EnemyMovement enemyMovement =>
        _enemyMovement ?? (_enemyMovement = GetComponent<EnemyMovement>());

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Bullet bullet = collision.GetComponent<Bullet>();
            TakeDamage(bullet.damage);
        }
    }

    // 적 움직임
    private void Move()
    {
        Vector2 direction = enemyMovement.GetDirection();
        rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
    }

    // 적 데미지 받음
    public void TakeDamage(int damage)
    {
        // 적이 죽었으면 데미지를 받지않음
        if (isDead) return;

        hp -= damage;

        if(hp <= 0)
        {
            hp = 0;
            isDead = true;
            Die();
        }
    }

    private void OnEnable()
    {
        enemyMovement.OnArrive += Arrive;
    }

    private void Arrive(EnemyMovement enemyMovement)
    {
        Destroy(gameObject);
    }

    // 적 죽음
    private void Die()
    {
        OnDie?.Invoke();
        OnDropCoin?.Invoke(reawrd);
        Destroy(gameObject);
    }
}
