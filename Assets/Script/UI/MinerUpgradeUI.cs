using System.Collections;
using UnityEngine;

public class MinerUpgradeUI : MonoBehaviour
{
    public static MinerUpgradeUI Instance;
    public GameObject uiPanel;
    private Miner targetMiner;

    private void Awake()
    {
        Instance = this;
        uiPanel.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(WaitStart());
    }

    IEnumerator WaitStart()
    {
        yield return null;

        if (targetMiner == null)
        {
            targetMiner = Object.FindFirstObjectByType<Miner>();
        }

        UpdateUI();
    }

    public void Show(Miner miner)
    {
        targetMiner = miner;
        uiPanel.SetActive(true);


        uiPanel.transform.position = Camera.main.WorldToScreenPoint(miner.transform.position + Vector3.up);

    }

    public void Hide()
    {
        uiPanel.SetActive(false);
    }

    public void UpdateUI()
    {
        if (targetMiner != null)
        {
            UiManager.instance.minerSpeedText.text = $"Speed : {targetMiner.miningSpeed:F1}";
            UiManager.instance.minerUpgradeText.text = $"UPCost : {targetMiner.upgradeCost}";
            UiManager.instance.hpText.text = $"{targetMiner.currentHp:F0}";
        }
    }

    public void OnUpgradeButtonClick()
    {
        if (targetMiner != null)
        {
            targetMiner.UpgradeMiner();

            UpdateUI();
        }
    }
}
