using UnityEngine;

// 가장 가까운 타겟을 감지하는 스크립트
public class TargetSearcher : MonoBehaviour
{
    public float detectRadius;                            // 타겟 감지 범위

    [SerializeField] private LayerMask enemyLayer;        // 타겟 레이어 설정

    public Transform currentTarget { get; private set; }  // 현재 타겟 위치 정보


    // 타겟 감지 범위에 들어온 타겟을 리스트로 받고 첫번째 리스트의 위치를 반환
    public Transform Search()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, detectRadius, enemyLayer);

        if(hits.Length == 0)
        {
            return null;
        }
        else
        {
            return hits[0].transform;
        }
    }
}
