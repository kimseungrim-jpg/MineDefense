using UnityEngine;

public class TowerSelectManager : MonoBehaviour
{
    public static TowerSelectManager instance;

    public Tower selectedTower;

    private void Awake()
    {
        instance = this;
    }

    public void SelectedTower(Tower tower)
    {
        if (selectedTower != null)
        {
            selectedTower.SetSelectTowerVisible(false);
        }

        selectedTower = tower;
        selectedTower.SetSelectTowerVisible(true);
    }

    public void OnUpgradeButtonClikc()
    {
        if (selectedTower != null)
        {
            selectedTower.Upgrade();

            Clear();
        }else
        {
            Debug.Log("업그레이드 실패 : 선택된 타워가 없음!!!");
        }
    }

    public void Clear()
    {
        if (selectedTower != null)
        {
            selectedTower.SetSelectTowerVisible(false);
        }
        selectedTower = null;
    }
}
