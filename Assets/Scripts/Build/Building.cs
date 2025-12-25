using System;
using UnityEngine;
using UnityEngine.UI;

// BuildingData 정보를 받아 월드에 건물을 설치하는 스크립트
public class Building : MonoBehaviour
{
    // BuildingData 의 건물 정보를 받아옴
    public string Description => data.Description;

    // BuildingData 의 건물 비용을 받아옴
    public int Cost => data.Cost;

    // 건물을 설치했을때 이벤트
    public static event Action OnBuild;

    private BuildingModel model;

    private BuildingData data;

    public void Setup(BuildingData data)
    {
        this.data = data;
        model = Instantiate(data.Model, transform.position, Quaternion.identity, transform);
        OnBuild?.Invoke();
    }
}
