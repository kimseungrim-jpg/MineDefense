using TMPro;
using UnityEngine;

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
    void Update()
    {
        goldText.text = $"Gold : {GoldManager.instance.gold}";
        oreText.text = $"Ore : {OreManager.instance.ore}";
        waveText.text = $"WAVE : {WaveManager.instance.currentWave}";
    }

    public void ShowGameOver()
    {
        gameOverPannel.SetActive(true);
    }
}
