using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// BuildingCell 정보를 받아 건물이 몇칸을 차지하는지 계산
public class BuildingModel : MonoBehaviour
{
    [SerializeField] private Transform point; // 건물 프리팹과 건물셀의 기준점

    private BuildingCell[] buildingCell;
    private void Awake()
    {
        buildingCell = GetComponentsInChildren<BuildingCell>();
    }

    public List<Vector3> GetAllBuidingPositions()
    {
        return buildingCell.Select(cell => cell.transform.position).ToList();
    }
}
