using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 음량 설정 창을 열고 닫으며 슬라이더 입력을 AudioManager에 전달
/// 창을 열 때 현재 음량을 표시하고 닫을 때 변경한 설정을 저장
/// </summary>
public class AudioSettingsController : MonoBehaviour
{
    [Header("설정 창")]
    [SerializeField] private GameObject settingsPanel;

    [Header("음량 슬라이더")]
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    /// <summary>
    /// 필요한 UI 연결을 확인하고 설정 창을 닫은 상태로 준비
    /// </summary>
    private void Awake()
    {
        if (settingsPanel == null || bgmSlider == null || sfxSlider == null)
        {
            Debug.LogError("[AudioSettingsUI] 설정 패널과 BGM, SFX 슬라이더 연결을 확인해 주세요.", this);
            enabled = false;
            return;
        }

        settingsPanel.SetActive(false);

        // 슬라이더 값이 변경 될때만 음량 적용되도록 이벤트 연결
        bgmSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
    }

    /// <summary>
    /// 설정 버튼을 누르면 현재 음량을 슬라이더에 표시하고 설정 창을 열음
    /// </summary>
    public void Open()
    {
        if (!enabled)
            return;

        AudioManager audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("[AudioSettingsUI] 씬에 AudioManager가 필요합니다.", this);
            return;
        }

        bgmSlider.SetValueWithoutNotify(audioManager.BgmVolume);
        sfxSlider.SetValueWithoutNotify(audioManager.SfxVolume);

        settingsPanel.SetActive(true);
        audioManager.PlaySfx(AudioManager.SfxType.ButtonClick);
    }

    /// <summary>
    /// 닫기 버튼을 누르면 변경한 음량을 저장하고 설정 창을 닫음
    /// </summary>
    public void Close()
    {
        if (!enabled || !settingsPanel.activeSelf)
            return;

        if (AudioManager.instance != null)
        {
            AudioManager.instance.SaveVolumeSettings();
            AudioManager.instance.PlaySfx(AudioManager.SfxType.ButtonClick);
        }

        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// BGM 슬라이더의 변경된 값을 전달해 재생 중인 배경음의 음량을 조절
    /// </summary>
    private void OnBgmVolumeChanged(float value)
    {
        if(AudioManager.instance != null)
            AudioManager.instance.SetBgmVolume(value);
    }

    /// <summary>
    /// SFX 슬라이더의 변경된 값을 전달해 효과음 그룹의 음량을 조절
    /// </summary>
    private void OnSfxVolumeChanged(float value)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.SetSfxVolume(value);
    }

    /// <summary>
    /// 오브젝트가 제거될 때 직접 등록한 슬라이더 이벤트를 해제
    /// </summary>
    private void OnDestroy()
    {
        if(bgmSlider != null)
            bgmSlider.onValueChanged.RemoveListener(OnBgmVolumeChanged);

        if (sfxSlider != null)
            sfxSlider.onValueChanged.RemoveListener(OnSfxVolumeChanged);
    }
}
