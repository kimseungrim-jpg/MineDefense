using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;

    public int gold = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 전달받은 수량을 현재 보유 골드에 추가
    /// 적이 처치되어 보상을 지급할 때 Enemy에서 호출
    /// </summary>
    public void AddGold(int amount)
    {
        gold += amount;
    }
}
