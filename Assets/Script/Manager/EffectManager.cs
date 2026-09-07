using UnityEngine;

/// <summary>
/// 타워와 광부의 강화 결과를 보여주는 일회성 시각 효과를 생성하고 제거
/// 강화 로직은 각 대상이 담당, 해당 클래스에서는 효과의 수명만 관리
/// </summary>
public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    public GameObject levelupPrefab;
    public GameObject hpUpPrefab;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 전달받은 월드 위치에 레벨 상승 효과를 생성하고 2초 뒤 제거
    /// 타워 또는 광부의 일반 강화가 완료된 직후 호출
    /// </summary>
    public void PlayerLevelupEffect(Vector3 position)
    {
        if (levelupPrefab != null)
        {
            GameObject effect = Instantiate(levelupPrefab, position, Quaternion.identity);

            Destroy(effect, 2f);
        }
    }

    /// <summary>
    /// 전달받은 월드 위치에 체력 회복 효과를 생성하고 2초 뒤 제거
    /// 광부가 최대 강화 상태에서 체력을 회복한 직후 호출
    /// </summary>
    /// <param name="position"></param>
    public void PlayerHpUpEffect(Vector3 position)
    {
        if (levelupPrefab != null)
        {
            GameObject effect2 = Instantiate(hpUpPrefab, position, Quaternion.identity);

            Destroy(effect2, 2f);
        }
    }
}
