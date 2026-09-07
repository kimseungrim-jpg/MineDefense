using UnityEngine;

/// <summary>
/// 현재 선택된 타워를 지정하고 타워의 선택 표시 활성 형태를 관리
/// 새로운 타워가 선택되면 이전 선택 표시를 해제하여 한 번에 하나의 타워만 선택되도록 설정
/// </summary>
public class TowerSelectManager : MonoBehaviour
{
    public static TowerSelectManager instance;

    public Tower selectedTower;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 기존 타워의 선택 표시를 끄고 전달받은 타워를 새로운 선택 대상으로 등록
    /// Tower 또는 BuildSlot이 배치된 타워의 클릭을 처리할 때 초훌
    /// </summary>
    public void SelectedTower(Tower tower)
    {
        //이미 선택된 타워가 있다면 해당 타워의 정보를 종료
        if (selectedTower != null)
        {
            selectedTower.SetSelectTowerVisible(false);
        }

        selectedTower = tower;
        selectedTower.SetSelectTowerVisible(true);
    }

    /// <summary>
    /// 현재 타워의 선택 표시를 끄고 선택 참조를 비움
    /// 빈 배경 클릭이나 다른 UI 상호작용으로 타워 선택을 해제할 때 호출
    /// </summary>
    public void Clear()
    {
        if (selectedTower != null)
        {
            selectedTower.SetSelectTowerVisible(false);
        }
        selectedTower = null;
    }
}
