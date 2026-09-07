using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 빈 배경을 클릭했을 때 현재 선택된 타워, 광부 및 건설 UI 상태를 해제
/// UI 위에서 발생한 클릭은 선택 해제로 처리하지 않아 버튼 조작과 월드 클릭이 충돌하지 않도록 설정
/// </summary>
public class ClickOutSide : MonoBehaviour
{
    /// <summary>
    /// 오브젝트의 Collider2D를 마우스 클릭시 열려 있는 선택,건설 UI를 정리하는 함수
    /// 상호작용이 없는 화면의 빈 영역 역할을 하는 배경 오브젝트에서 호출
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TowerSelectManager.instance != null)
        {
            TowerSelectManager.instance.Clear();

        }

        if (MinerUpgradeUI.Instance != null)
        {
            MinerUpgradeUI.Instance.Hide();
        }

        FindAnyObjectByType<BuildUI>()?.CloseBuildMenu();

        TowerInfoUI.instance?.Hide();

        if (BuildManager.instance != null)
        {
            BuildManager.instance.ClearSelection();
            BuildManager.instance.RefreshAllBuildButtons();
        }
    }
}
