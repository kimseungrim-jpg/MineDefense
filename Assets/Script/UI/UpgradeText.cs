using UnityEngine;

/// <summary>
/// 강화와 체력 회복 효과를 위로 이동시키고 일정 시간이 지나면 제거
/// </summary>
public class UpgradeText : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destoryTime = 1f;

    
    /// <summary>
    /// 효과가 생성되면 설정된 시간이 지난 뒤 현재 오브젝트가 제거되도록 설정
    /// </summary>
    void Start()
    {
        Destroy(gameObject, destoryTime);
    }

    /// <summary>
    /// 효과가 표시되는 동안 매 프레임마다 위쪽으로 이동
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
}
