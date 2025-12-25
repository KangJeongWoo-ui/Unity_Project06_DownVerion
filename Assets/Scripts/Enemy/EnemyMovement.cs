using System;
using System.Linq;
using UnityEngine;

// 적 캐릭터 이동경로를 계산하는 스크립트
public class EnemyMovement : MonoBehaviour
{
    private Waypoint waypoint;

    int waypointIndex = 0;

    // 적이 마지막 wayPoint에 도달할떄 호출되는 이벤트
    public event Action<EnemyMovement> OnArrive;

    // 적이 마지막 wayPoint에 도달하면 플레이어에게 데미지를 주기위한 이벤트
    public static Action OnPlayerDamaged;

    private void Awake()
    {
        if (waypoint == null)
        {
            waypoint = FindFirstObjectByType<Waypoint>();
        }
    }
    public Vector2 GetDirection()
    {
        // waypoint를 찾지못하면 멈춤
        if (waypoint == null) return Vector2.zero;

        // 초기 waypoint를 목표로 정함
        Transform target = waypoint.waypointPosition[waypointIndex];

        // 현재 캐릭터 위치에서 다음 waypoint 까지의 방향 계산
        Vector2 direction = (target.position - transform.position).normalized;

        // 현재 캐릭터 위치에서 다음 waypoint 까지의 거리 계산
        float distance = Vector2.Distance(transform.position, target.position);

        // 캐릭터와 waypoint 간의 거리가 0.1 미만이라면(waypoint에 도착하면) 다음 waypoint를 목표로 함
        if(distance < 0.1f)
        {
            waypointIndex++;

            if (waypointIndex >= waypoint.waypointPosition.Length)
            {
                OnArrive?.Invoke(this);
                OnPlayerDamaged?.Invoke();
                return Vector2.zero;
            }
        }

        // waypoint 까지의 방향 전달
        return direction;
    }
}
