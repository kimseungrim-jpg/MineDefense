using TMPro;
using UnityEngine;

/// <summary>
/// 플레이 중 표시되는 골드, 광석, 웨이브 정보와 게임 오버 패널을 관리
/// 광부의 체력과 강화 정보는 MinerUpgradeUI가 이 클래스의 텍트를 참조해 갱신
/// </summary>
public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public TMP_Text goldText;
    public TMP_Text oreText;
    public TMP_Text waveText;
    public TMP_Text minerSpeedText;
    public TMP_Text minerUpgradeText;
    public GameObject gameOverPannel;
    public TMP_Text hpText;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 매 프레임 GoldMnager, OreManager, WaveManager의 현재 값을 화면에 반영
    /// </summary>
    void Update()
    {
        goldText.text = $"Gold : {GoldManager.instance.gold}";
        oreText.text = $"Ore : {OreManager.instance.ore}";
        waveText.text = $"WAVE : {WaveManager.instance.currentWave}";
    }

    /// <summary>
    /// 게임 오버 패널을 화면에 표시
    /// 광부의 체력이 모두 소진되어 GameManager.GameOver()가 실행될 때 호출
    /// </summary>
    public void ShowGameOver()
    {
        gameOverPannel.SetActive(true);
    }
}
