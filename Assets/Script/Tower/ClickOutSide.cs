using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOutSide : MonoBehaviour
{
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
