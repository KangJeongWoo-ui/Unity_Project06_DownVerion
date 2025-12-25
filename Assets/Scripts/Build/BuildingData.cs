using UnityEngine;
[CreateAssetMenu(menuName = "Data/Building")]

// 건물 데이터 정의 스크립트
public class BuildingData : ScriptableObject
{
    [field: SerializeField] public string Description {  get; private set; }  // 건물 정보
    [field: SerializeField] public int Cost { get; private set; }             // 건물 비용
    [field: SerializeField] public BuildingModel Model { get; private set; }  // 건물 형태
}
