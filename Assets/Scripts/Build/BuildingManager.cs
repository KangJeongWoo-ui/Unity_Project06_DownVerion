using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 건물 설치 관리 스크립트
public class BuildingManager : MonoBehaviour
{
    // 타일맵 한칸의 크기
    public const float CellSize = 1f;

    [SerializeField] private BuildingData buildingData1;
    [SerializeField] private BuildingData buildingData2;
    [SerializeField] private BuildingData buildingData3;

    // 건물 미리보기 프리팹
    [SerializeField] private BuildingPreview previewPrefab;

    // 설치될 건물 프리팹
    [SerializeField] private Building buildingPrefab;

    // 타일맵
    [SerializeField] private BuildingGrid grid;

    private BuildingPreview preview;

    private void Update()
    {
        // 현재 마우스 위치 = 월드 좌표
        Vector3 mousPos = GetMouseWorldPosition();

        if(preview != null )
        {
            HandlePreview(mousPos);
        }
        else
        {
            // 1번 키: 건물 1 미리보기 생성
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                preview = CreatePreview(buildingData1, mousPos);
            }
            // 2번 키: 건물 2 미리보기 생성
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                preview = CreatePreview(buildingData2, mousPos);
            }
            // 3번 키: 건물 3 미리보기 생성
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                preview = CreatePreview(buildingData3, mousPos);
            }
        }
    }
    private void HandlePreview(Vector3 moustWorldPosition)
    {
        // 미리보기 위치를 마우스 위치로 이동
        preview.transform.position = moustWorldPosition;

        List<Vector3> buildPostions = preview.BuildingModel.GetAllBuidingPositions();

        // 현재 위치에 건물 설치가 가능한지 확인
        bool canBuild = grid.CanBuild(buildPostions);


        if(canBuild)
        {
            // 건물 위치 보정
            preview.transform.position = GetSnappedCenterPosition(buildPostions);

            // 미리보기 상태를 설치가능으로 변경
            preview.ChangeState(BuildingPreview.BuildingPreviewState.Positive);

            // 마우스 좌클릭으로 건물 설치
            if(Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(buildPostions);
            }
        }
        else
        {
            // 설치가 불가능 하면 미리보기 상태를 설치불가능으로 변경
            preview.ChangeState(BuildingPreview.BuildingPreviewState.Negative);
        }
    }
    private void PlaceBuilding(List<Vector3> buildingPositions)
    {
        // 건물 프리팹 생성
        Building building = Instantiate(buildingPrefab, preview.transform.position, Quaternion.identity);

        // 타일맵에 건물 정보 저장
        grid.SetBuilding(building, buildingPositions);

        // 건물 설치
        building.Setup(preview.Data);

        // 미리보기 제거
        Destroy(preview.gameObject);
        preview = null;
    }
    private Vector3 GetSnappedCenterPosition(List<Vector3> allBuildingPostions)
    {
        // 타일의 x,y 좌표를 정수로 변환
        List<int> x = allBuildingPostions.Select(p =>Mathf.FloorToInt(p.x)).ToList();
        List<int> y = allBuildingPostions.Select(p => Mathf.FloorToInt(p.y)).ToList();

        // 건물이 설치될 위치의 중앙 계산
        float centerX = (x.Min() + x.Max() + 1) * 0.5f * CellSize;
        float centerY = (y.Min() + y.Max() + 1) * 0.5f * CellSize;
        return new(centerX, centerY, 0);
    }

    // 마우스 좌표를 월드 좌표로 변환
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 0f;

        return worldPos;
    }

    // 건물 미리보기 생성
    private BuildingPreview CreatePreview(BuildingData data, Vector3 position)
    {
        BuildingPreview buildingPreview = Instantiate(previewPrefab, position, Quaternion.identity);
        buildingPreview.Setup(data);
        return buildingPreview;
    }
}
