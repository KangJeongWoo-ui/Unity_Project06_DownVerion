using UnityEngine;

// 가장 가까운 타겟을 탐지하고 회전하는 스크립트
public class TurretController : MonoBehaviour
{
    private TargetSearcher _targetSearcher;
    private TargetRotator _targetRotator;
    private TargetShooter _targetShooter;

    private Transform target;

    private bool isAction = false;

    public TargetSearcher targetSearcher =>
        _targetSearcher ?? (_targetSearcher = GetComponent<TargetSearcher>());

    public TargetRotator targetRotator =>
        _targetRotator ?? (_targetRotator = GetComponent<TargetRotator>());

    public TargetShooter targetShooter =>
        _targetShooter ?? (_targetShooter = GetComponent<TargetShooter>());


    private void OnEnable()
    {
        Building.OnBuild += Action;
    }
    private void Action()
    {
        isAction = true;
    }
    void Update()
    {
        if (!isAction) return;

        // 1. 타겟이 없으면 탐색
        if (target == null)
        {
            target = targetSearcher.Search();
            targetShooter.SetTarget(target);
            return;
        }

        // 2. 방향 체크
        float distance = Vector2.Distance(transform.position, target.position);
        if (distance > targetSearcher.detectRadius)
        {
            target = null;
            targetShooter.SetTarget(null);
            return;
        }

        // 3. 회전
        targetRotator.RotateTo(target);
    }
}
