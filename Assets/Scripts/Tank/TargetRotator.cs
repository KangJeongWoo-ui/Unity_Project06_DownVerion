using UnityEngine;

// 감지한 타겟의 위치 방향으로 물체 방향을 회전시키는 스크립트
public class TargetRotator : MonoBehaviour
{
    public void RotateTo(Transform target)
    {
        if (target == null) return;

        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}
