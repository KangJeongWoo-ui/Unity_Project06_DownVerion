using System.Collections;
using UnityEngine;

// 감지한 타겟을 향해 총알을 발사하는 스크립트
public class TargetShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;    // 총알 프리팹
    [SerializeField] private Transform firePoint;        // 발사 위치
    [SerializeField] private float fireInterval;         // 발사 간격
    [SerializeField] private float initalFireDelay;      // 초기 발사 지연 시간

    private float nextFireTime;                          // 다음 발사 간격
    private Transform currentTarget;                            // 타겟

    // 전달 받은 새로운 타겟을 타겟으로 설정
    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
    }

    private void Update()
    {
        if (currentTarget == null) return;

        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireInterval;
            StartCoroutine(FireDelay());
        }
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(initalFireDelay);

        if (currentTarget != null)
            Fire();
    }

    private void Fire()
    {
        GameObject bullet =
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Bullet bulletComp = bullet.GetComponent<Bullet>();
        bulletComp.target = currentTarget;
    }
}
