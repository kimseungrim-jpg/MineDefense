using UnityEngine;

/// <summary>
/// 건설 메뉴의 on/off 상태를 관리
/// 건설 메뉴의 메인 버튼 상태와 타워 선택 패널(버튼)들의 활성 상태를 서로 전환
/// </summary>
public class BuildUI : MonoBehaviour
{
    public GameObject mainOpenButton;
    public GameObject towerButtonPanel;

    /// <summary>
    /// 타워 선택 패널을 표시하고 메인 버튼을 숨김
    /// Game 씬의 건설 메뉴 열기 버튼이 클릭되었을 때 호출
    /// </summary>
    public void OpenBuildMenu()
    {
        mainOpenButton.SetActive(false);
        towerButtonPanel.SetActive(true);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.ButtonClick);
        }
    }

    /// <summary>
    /// 타워 선택 패널을 닫고 메인 열기 버튼을 다시 표시
    /// 빈 배경을 클릭해 건설 메뉴를 닫을 때 ClickOutSide에서 호출, 진행 중인 건설 선택도 해제
    /// </summary>
    public void CloseBuildMenu()
    {
        mainOpenButton.SetActive(true);
        towerButtonPanel.SetActive(false);

        if (BuildManager.instance != null)
        {
            BuildManager.instance.ClearSelection();
        }
    }
}
