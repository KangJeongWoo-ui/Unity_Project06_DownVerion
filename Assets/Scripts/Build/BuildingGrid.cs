using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// 건물을 설치할수 있는 타일맵 구조 스크립트
public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private int width;   // 타일맵 가로 크기

    [SerializeField] private int height;  // 타일맵 세로 크기

    [SerializeField] private LayerMask roadLayer;

    private BuildingGridCell[,] grid;     // 타일맵 2차원 배열


    // 시작시 가로, 세로 비율로 타일맵 초기화
    private void Start()
    {
        grid = new BuildingGridCell[width, height];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                grid[x, y] = new();
            }
        }
    }

    // 건물을 타일맵에 배치하는 함수
    public void SetBuilding(Building building, List<Vector3> allBuildingPositions)
    {
        // 타일맵에 설치된 모든 좌표값 확인
        foreach (var position in allBuildingPositions)
        {
            // 월드 좌표값을 타일맵 좌표값으로 변환
            (int x, int y) = WorldToGridPosition(position);

            // 타일맵 좌표로 건물 저장
            grid[x, y].SetBuilding(building);
        }
    }

    // 건물을 타일맵에 배치할 수 있는지 확인
    public bool CanBuild(List<Vector3> allBuildingPositions)
    {
        foreach (var position in allBuildingPositions)
        {
            // 월드 좌표값을 타일맵 좌표값으로 변환
            (int x, int y) = WorldToGridPosition(position);

            // 타일 좌표값을 넘어가면 설치 불가
            if (x < 0 || x >= width || y < 0 || y >= height) return false;

            // 이미 타일에 건물이 설치되어 있다면 설치 불가
            if (!grid[x, y].IsEmpty()) return false;

            // 길이라면 건물 설치 불가
            if (IsRoad(position)) return false;
        }
        return true;
    }

    // 설치 위치가 길인지 확인
    private bool IsRoad(Vector3 position)
    {
        Collider2D hit = Physics2D.OverlapCircle(
            position,
            0.5f,
            roadLayer
        );

        return hit != null;
    }

    // 월드 좌표값을 타일맵 좌표값으로 변환하는 함수
    private (int x, int y) WorldToGridPosition(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - transform.position).x / BuildingManager.CellSize);
        int y = Mathf.FloorToInt((worldPosition - transform.position).y / BuildingManager.CellSize);
        return (x, y);
    }

    // 에디터에 타일맵 시각화
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (BuildingManager.CellSize <= 0 || width <= 0 || height <= 0) return;
        Vector3 origin = transform.position;

        for(int y = 0; y <= height; y++)
        {
            Vector3 start = origin + new Vector3(0, y * BuildingManager.CellSize, 0);
            Vector3 end = origin + new Vector3(width * BuildingManager.CellSize, y * BuildingManager.CellSize, 0);
            Gizmos.DrawLine(start, end);
        }
        for(int x = 0; x<= width; x++)
        {
            Vector3 start = origin + new Vector3(x * BuildingManager.CellSize, 0, 0);
            Vector3 end = origin + new Vector3(x * BuildingManager.CellSize, height * BuildingManager.CellSize, 0);
            Gizmos.DrawLine(start, end);
        }
    }
    public class BuildingGridCell
    {
        // 배치 될 건물
        private Building building;
        public void SetBuilding(Building building)
        {
            this.building = building; 
        }

        // 타일 맵이 비어있는지 확인
        public bool IsEmpty()
        {
            return building == null;
        }
    }
}
