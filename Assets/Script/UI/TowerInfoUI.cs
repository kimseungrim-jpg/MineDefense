using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 선택된 타워의 이름과 능력치를 표시하고 강화 및 판매 버튼 입력을 처리
/// 판매 시 타워가 배치된 BuildSlot을 통해 타워를 제거할 수 있도록 타워와 슬롯을 함께 저장
/// </summary>
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

    /// <summary>
    /// 선택된 타워와 해당 BuildSlot을 저장하고 현재 타워 정보를 패널에 표시
    /// Tower 또는 BuildSlot이 배치된 타워의 클릭을 처히라 때 호출
    /// </summary>
    public void Show(Tower tower, BuildSlot slot)
    {
        targetTower = tower;
        targetSlot = slot;

        nameText.text = tower.towerName;
        statText.text = tower.GetStats();

        modalBackground.SetActive(true);
        infoPanel.SetActive(true);
    }

    /// <summary>
    /// 저장된 타워와 슬롯 참조는 유치한 채 정보 패널과 모달 배경을 숨김
    /// 판매 완료 또는 빈 배경 클릭으로 타워 정보 표시를 종료할 때 호출
    /// </summary>
    public void Hide()
    {
        modalBackground.SetActive(false);
        infoPanel.SetActive(false);
    }

    /// <summary>
    /// 현재 선택된 타워에 강화를 요청하고 변경된 능력치를 다시 표시
    /// 타워 정보 패널의 강화 버튼이 클릭되었을 때 호출
    /// </summary>
    public void OnUpgradeClick()
    {
        if (targetTower != null)
        {
            targetTower.Upgrade();
            statText.text = targetTower.GetStats();
        }
    }

    /// <summary>
    /// 선택된 타워의 판매 금액을 지급하고 해당 BuildSlot에서 타워를 제거
    /// 타워 정보 패널의 판매 버튼이 클릭되었을 때 호출
    /// </summary>
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
