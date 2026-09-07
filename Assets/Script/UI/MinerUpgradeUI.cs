using System.Collections;
using UnityEngine;

/// <summary>
/// 광부의 체력, 체굴 속도 및 강화 비용을 표시하고 광부 강화 입력을 전달
/// 광부가 선택되면 해당 위치에 패널을 표시하며 타워 정보 UI와 교대로 사용
/// </summary>
public class MinerUpgradeUI : MonoBehaviour
{
    public static MinerUpgradeUI Instance;
    public GameObject uiPanel;
    private Miner targetMiner;

    /// <summary>
    /// 게임 시작 시 광부가 선택되지 않은 상태로 UI를 초기화
    /// </summary>
    private void Awake()
    {
        Instance = this;
        uiPanel.SetActive(false);
    }

    /// <summary>
    /// 초기화가 끝난 뒤 광부를 탐색하도록 대기 코루틴을 시작
    /// </summary>
    private void Start()
    {
        StartCoroutine(WaitStart());
    }

    /// <summary>
    /// 한 프레임 대기한 뒤 씬의 광부를 찾아 저장하고 초기 UI 값을 갱신
    /// Start에서 코루틴으로 실행되어 다른 오브젝트의 초기화와 겹치지 않도록 함
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitStart()
    {
        yield return null;

        if (targetMiner == null)
        {
            targetMiner = Object.FindFirstObjectByType<Miner>();
        }

        UpdateUI();
    }

    /// <summary>
    /// 전달받은 광부를 현재 대상으로 짖어하고 광부의 월드 위치 위에 UI 패널을 표시
    /// 광부 오브젝트가 클릭되었을 때 Miner에서 호출
    /// </summary>
    public void Show(Miner miner)
    {
        targetMiner = miner;
        uiPanel.SetActive(true);


        uiPanel.transform.position = Camera.main.WorldToScreenPoint(miner.transform.position + Vector3.up);

    }

    /// <summary>
    /// 현재 광부 참조는 유지한 채 광부 정보 패널만 숨김
    /// 타워를 선택하거나 빈 배경을 클릭했을 때 호출
    /// </summary>
    public void Hide()
    {
        uiPanel.SetActive(false);
    }

    /// <summary>
    /// 현재 광부의 채굴 속도, 다음 강화 비용 및 체력을 화면에 반영
    /// 초기화, 피격 또는 강화로 광부 상태가 변경된 뒤 호출
    /// </summary>
    public void UpdateUI()
    {
        if (targetMiner != null)
        {
            UiManager.instance.minerSpeedText.text = $"Speed : {targetMiner.miningSpeed:F1}";
            UiManager.instance.minerUpgradeText.text = $"UPCost : {targetMiner.upgradeCost}";
            UiManager.instance.hpText.text = $"{targetMiner.currentHp:F0}";
        }
    }

    /// <summary>
    /// 현재 선택된 광부에게 강화를 요청하고 변경된 정보를 즉시 갱신
    /// Game 씬의 광부 강화 버튼이 클릭되었을 때 호출
    /// </summary>
    public void OnUpgradeButtonClick()
    {
        if (targetMiner != null)
        {
            targetMiner.UpgradeMiner();

            UpdateUI();
        }
    }
}
