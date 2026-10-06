using UnityEngine;

/// <summary>
/// 전사 모델의 공격 애니메이션 이벤트를 WarriorAttack에 전달
/// 자식 Animator에서 발생한 공격 적중 시점과 부모의 범위 피해 로직을 연결
/// </summary>
public class WarriorATKani : MonoBehaviour
{
    WarriorAttack attack;

    void Awake()
    {
        attack = GetComponentInParent<WarriorAttack>();
    }

    /// <summary>
    /// 무기가 적중하는 공격 애니메이션 프레임에서 호출
    /// 실제 범위 검색과 피해 처리는 WarriorAttack.Dealing에서 수행
    /// </summary>
    public void Attack()
    {
        attack.Dealing();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.WarriorAttack);
        }
    }
}
