using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 현재 수행할 수 있는 건설 상호작용 상태를 나타내는 열거형 함수
/// None은 일반 선택 상태, Build는 선택된 타워를 건설하는 상태
/// </summary>
public enum BuildMode
{
    None,
    Build
}

/// <summary>
/// 건설할 타워 프리팹과 현재 건설 모드를 관리하고 타워 선택 버튼의 표시 상태를 갱신
/// TowerSelectButton과 BuildSolt 사이에서 건설 선택 정보를 공휴나느 역할 담당
/// </summary>
public class BuildManager : MonoBehaviour
{
    public static BuildManager instance; //싱글톤 생성

    public GameObject SelectedTowerPrefab; //선택된 타워 프리팹
    public BuildMode CurrentMode = BuildMode.None;

    public List<TowerSelectButton> towerSelectButtons = new List<TowerSelectButton>();

    /// <summary>
    /// Awake가 호출될 때 현재 BuildManager를 전역 접근 인스턴스로 등록
    /// 이미 다른 인스턴스가 존재한다면 중복된 건설 상태 관리를 방지하기 위해 현재 오브젝트를 제거
    /// </summary>
    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

       
    }
    
    /// <summary>
    /// 건설 버튼으로 선택한 타워 프리팹을 저장하고 건설 모드로 전환하는 함수
    /// TowerSelectButton이 클릭되었을 때 호출되며, 건설 비용이 부족하면 선택을 해제
    /// </summary>
    public void SelectTower(GameObject towerPrefab) // 타워 선택
    {
        Tower cost = towerPrefab.GetComponent<Tower>();
        if (cost != null && GoldManager.instance.gold >= cost.buildCost)
        {
            SelectedTowerPrefab = towerPrefab;
            CurrentMode = BuildMode.Build;
        }else
        {
            ClearSelection();
        }

        RefreshAllBuildButtons();
    }

    /// <summary>
    /// 선택된 타워 프리팹을 비우고 일반 선택 상태로 돌리는 함수
    /// 건설 메뉴를 닫거나 보유 골드가 부족해 건설을 계속할 수 없을 때 호출
    /// </summary>
    public void ClearSelection()
    {
        SelectedTowerPrefab = null;
        CurrentMode = BuildMode.None;
    }

    /// <summary>
    /// 등록된 모든 타워 건설 버튼의 선택 표시를 현재 건설 상태에 맞게 갱신하는 함수
    /// 타워 선택 또는 건설 결과로 선택 상태가 변경된 뒤 호출
    /// </summary>
    public void RefreshAllBuildButtons()
    {
        foreach (var btn in towerSelectButtons)
        {
            btn.UpdateVisual();
        }
    }
}
