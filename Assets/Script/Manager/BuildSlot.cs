using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 타워를 배치할 수 있는 한 칸의 건설 슬롯을 관리
/// 현재 건설 모드에 따라 타워를 생성하거나 슬롯에 배치된 타워를 선택
/// </summary>
public class BuildSlot : MonoBehaviour
{
    public GameObject currentTower;

    /// <summary>
    /// 슬롯의 Collider2D가 클릭되었을 때 현재 BuildMode에 맞는 상호작용을 실행하는 함수
    /// UI 위에서 발생한 클릭은 버튼 조작과 월드 건설이 동시에 처리되지 않도록 차단
    /// </summary>
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        switch (BuildManager.instance.CurrentMode)
        {
            case BuildMode.Build:
                BuildTower();
                break;
            case BuildMode.None:
                SelectCurrentTower();
                break;
        }
    }

    /// <summary>
    /// 현재 선택된 타워 프리팹을 슬롯 위치에 생성하고 건설 비용을 차감하는 함수
    /// 건설 모드에서 빈 Buildsolt을 클릭했을 때 OnMouseDown에서 호출
    /// </summary>
    void BuildTower()
    {
        // 한 슬롯에는 하나의 타워만 배치할 수 있으므로 이미 사용 중인 슬롯에는 건설 불가
        if (currentTower != null)
        {
            return;
        }
        if (BuildManager.instance.SelectedTowerPrefab == null)
        {
            return;
        }

        Tower tower = BuildManager.instance.SelectedTowerPrefab.GetComponent<Tower>();
        int cost = tower.buildCost;

        if (GoldManager.instance.gold >= cost)
        {
            GoldManager.instance.gold -= cost;

            currentTower = Instantiate(BuildManager.instance.SelectedTowerPrefab, transform.position, Quaternion.identity, transform);

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySfx(AudioManager.SfxType.ButtonClick);
            }

            //같은 타워를 더이상 건설할 비용이 없다면 건설 모드를 종료
            if (GoldManager.instance.gold < cost)
            {
                BuildManager.instance.ClearSelection();
            }
        }
        else
        {
            BuildManager.instance.ClearSelection();
        }

        BuildManager.instance.RefreshAllBuildButtons();
    }

    /// <summary>
    /// 현재 슬롯에 배치된 타워를 제거하고 현재 슬롯을 다시 건설 가능한 상태로 변경하는 함수
    /// TowerInfoUI의 판매 버튼에서 호출됨
    /// 판매 골드 지급은 UI에서 처리
    /// </summary>
    public void RemoveTower()
    {
        if (currentTower == null)
        {
            return;
        }

        Destroy(currentTower);
        currentTower = null;
    }

    /// <summary>
    /// 현재 슬롯에 배치된 타워를 선택하고 타워 정보 UI와 선택 표시를 갱신하는 함수
    /// 일반 선택 상태에서 타워가 배치된 Buildslot을 클릭했을 때 OnMouseDown에서 호출
    /// </summary>
    private void SelectCurrentTower()
    {
        if (currentTower == null)
        {
            return;
        }

        Tower tower = currentTower.GetComponent<Tower>();

        if (tower == null)
        {
            Debug.LogWarning(
                $"[BuildSlot] {currentTower.name}에 Tower 컴포넌트가 없습니다.",
                currentTower
            );
            return;
        }

        TowerInfoUI.instance?.Show(tower, this);
        MinerUpgradeUI.Instance?.Hide();

        if (TowerSelectManager.instance != null)
        {
            TowerSelectManager.instance.SelectedTower(tower);
        }
    }
}
