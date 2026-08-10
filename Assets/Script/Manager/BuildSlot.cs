using UnityEngine;
using UnityEngine.EventSystems;

public class BuildSlot : MonoBehaviour
{
    public GameObject currentTower;

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
            case BuildMode.Remove:
                RemoveTower();
                break;
            case BuildMode.None:
                SelectCurrentTower();
                break;
        }
    }

    void BuildTower()
    {
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

    public void RemoveTower()
    {
        if (currentTower == null)
        {
            return;
        }

        Destroy(currentTower);
        currentTower = null;
        //BuildManager.instance.CurrentMode = BuildMode.None;
    }

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
