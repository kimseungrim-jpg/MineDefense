using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 건설 메뉴에서 타워를 선택하는 버튼의 입력과 표시 색상을 관리
/// 클릭하면 이 버튼에 등록된 타워 프리팹을 BuildManager에 전달
/// </summary>
public class TowerSelectButton : MonoBehaviour
{
    public GameObject towerPrefab;

    public Image buttonImage;

    public Color normalColor = Color.white;
    public Color selectColor = Color.gray;

    private void Start()
    {
        UpdateVisual();
    }

    /// <summary>
    /// 버튼에 등록된 타워 프리팹을 건설 대상으로 선택
    /// Game 씬의 타워 선택 버튼을 클릭했을 때 호출
    /// </summary>
    public void SelectTower()
    {
        BuildManager.instance.SelectTower(towerPrefab);
    }

    /// <summary>
    /// 버튼의 타워가 현재 건설 대상으로 선택되어 있는지 확인하고 버튼 색상을 변경하는 함수
    /// </summary>
    public void UpdateVisual()
    {
        if (BuildManager.instance.SelectedTowerPrefab == towerPrefab && BuildManager.instance.CurrentMode == BuildMode.Build)
        {
            buttonImage.color = selectColor;
        }
        else
        {
            buttonImage.color = normalColor;
        }
    }
}
