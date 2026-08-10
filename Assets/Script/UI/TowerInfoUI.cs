using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUI : MonoBehaviour
{
    public static TowerInfoUI instance;

    public GameObject infoPanel;
    public GameObject modalBackground;

    public Text nameText;
    public Text statText;

    private Tower targetTower;
    private BuildSlot targetSlot;

    private void Awake()
    {
        instance = this;
        infoPanel.SetActive(false);
        modalBackground.SetActive(false);
    }

    public void Show(Tower tower, BuildSlot slot)
    {
        targetTower = tower;
        targetSlot = slot;

        nameText.text = tower.towerName;
        statText.text = tower.GetStats();

        modalBackground.SetActive(true);
        infoPanel.SetActive(true);
    }

    public void Hide()
    {
        modalBackground.SetActive(false);
        infoPanel.SetActive(false);
    }

    public void OnUpgradeClick()
    {
        if (targetTower != null)
        {
            targetTower.Upgrade();
            statText.text = targetTower.GetStats();
        }
    }

    public void OnRemoveClick()
    {
        if (targetTower != null && targetSlot != null)
        {
            GoldManager.instance.gold += targetTower.sellCost;

            targetSlot.RemoveTower();

            Hide();
        }
    }

}
