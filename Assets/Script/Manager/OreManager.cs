using UnityEngine;

/// <summary>
/// 플레이어가 보유한 광석을 관리하고 다른 시스템에 공통 광석 저장소를 제공
/// 광부가 생산한 광석을 누적하며 타워 강화 비용은 이 값을 기준으로 처리
/// </summary>
public class OreManager : MonoBehaviour
{
    public static OreManager instance;

    public int ore = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 전달받은 수량을 현재 보유 광석에 추가
    /// 웨이브 진행 중 광부의 생산 주기가 완료되었을 때 Miner에서 호출
    /// </summary>
    public void AddOre(int amount) 
    {
        ore += amount;
    }
}
