using System.Collections.Generic;
using UnityEngine;

// 마우스를 따라다니는 미리보기 오브젝트 생성 스크립트
public class BuildingPreview : MonoBehaviour
{
    public enum BuildingPreviewState
    {
        Positive, // 설치 가능

        Negative  // 설치 불가능
    }

    // 설치 가능은 반투명한 초록색
    [SerializeField] private Color positiveColor = new Color(0f, 1f, 0f, 0.5f);

    // 설치 불가능은 반투명한 빨간색
    [SerializeField] private Color negativeColor = new Color(1f, 0f, 0f, 0.5f);

    public BuildingPreviewState State { get; private set; } = BuildingPreviewState.Negative;

    public BuildingData Data { get; private set; }
    public BuildingModel BuildingModel { get; private set; }

    private List<SpriteRenderer> renderers = new();
    private List<Collider2D> colliders = new();

    // 건물 미리보기 오브젝트 생성
    public void Setup(BuildingData data)
    {
        Data = data;
        BuildingModel = Instantiate(
            data.Model,
            transform.position,
            Quaternion.identity,
            transform
            );

        renderers.AddRange(BuildingModel.GetComponentsInChildren<SpriteRenderer>());
        colliders.AddRange(BuildingModel.GetComponentsInChildren<Collider2D>());

        // 미리보기 오브젝트는 충돌 비활성화
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        // 미리보기 오브젝트 색 적용
        SetPreviewColor(State);

    }
    public void ChangeState(BuildingPreviewState newState)
    {
        // 상태가 같으면 변경하지 않음
        if (newState == State) return;

        // 상태 갱신
        State = newState;

        // 갱신된 상태의 색상으로 변경
        SetPreviewColor(State);
    }
    private void SetPreviewColor(BuildingPreviewState state)
    {
        // 상태에 따라 미리보기 색상 변경
        Color targetColor = state == BuildingPreviewState.Positive
            ? positiveColor
            : negativeColor;

        foreach (var renderer in renderers)
        {
            renderer.color = targetColor;
        }
    }
}
