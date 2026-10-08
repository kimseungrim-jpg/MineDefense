using UnityEngine;

/// <summary>
/// ESC 메뉴가 열린 동안 게임 시간을 정지하고 닫으면 다시 진행
/// 게임 오버 패널이 활성화되면 메뉴 입력을 차단
/// </summary>
public class GameMenuUI : MonoBehaviour
{
    [Header("인게임 메뉴")]
    [SerializeField] private GameObject menuPanel;

    [Header("기존 음량 설정 UI")]
    [SerializeField] private AudioSettingsController audioSettingsUI;
    [SerializeField] private GameObject settingsPanel;

    private float timeScaleBeforeMenu;
    private bool isMenuOpen;

    private void Awake()
    {
        if (menuPanel == null || audioSettingsUI == null || settingsPanel == null)
        {
            Debug.LogError("[GameMenuUI] 메뉴 패널, AudioSettingsUI, 설정 패널을 연결해 주세요.", this);
            enabled = false;
            return;
        }

        menuPanel.SetActive(false);
    }

    /// <summary>
    /// 매 프레임 ESC를 누른 순간을 체크하고 UI상태에 맞게 창을 닫거나 열음
    /// </summary>
    private void Update()
    {
        if (IsGameOverPanelOpen())
        {
            if (isMenuOpen)
                CloseMenu();

            return;
        }

        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (settingsPanel.activeInHierarchy)
        {
            audioSettingsUI.Close();
            return;
        }

        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    /// <summary>
    /// 메뉴가 닫힌 상태에서 ESC를 누르면 게임 시간을 정지하고 메뉴를 표시
    /// </summary>
    public void OpenMenu()
    {
        if (!enabled || isMenuOpen || IsGameOverPanelOpen())
            return;

        timeScaleBeforeMenu = Time.timeScale;
        Time.timeScale = 0f;
        isMenuOpen = true;

        menuPanel.SetActive(true);
        PlayClickSound();
    }

    /// <summary>
    /// 메뉴가 열린 상태에서 ESC를 누르면 메뉴를 닫고 이전 게임 속도로 복구
    /// 게임 오버 패널이 표시된 경우에는 정지 상태를 유지
    /// 별도의 메뉴 닫기 버튼에도 연결할 수 있음
    /// </summary>
    public void CloseMenu()
    {
        if (!enabled || !isMenuOpen)
            return;

        if (settingsPanel.activeInHierarchy)
        {
            audioSettingsUI.Close();
        }
        else
        {
            PlayClickSound();
        }

        menuPanel.SetActive(false);
        isMenuOpen = false;

        Time.timeScale = IsGameOverPanelOpen() ? 0f : timeScaleBeforeMenu;
    }

    /// <summary>
    /// 메뉴 입력과 시간 복구 전에 UiManager의 게임 오버 패널 여부를 확인
    /// </summary>
    private bool IsGameOverPanelOpen()
    {
        return UiManager.instance != null && UiManager.instance.gameOverPannel != null && UiManager.instance.gameOverPannel.activeInHierarchy;
    }

    private void PlayClickSound()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySfx(AudioManager.SfxType.ButtonClick);
    }
}
