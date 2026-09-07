using UnityEngine;

/// <summary>
/// 암살자 타워의 단일 대상 근접 공격을 담당
/// 공격 명령을 받으면 애니메이션을 재생
/// 애니메이션 이벤트가 호출되는 시점에 지정된 적에게 직접 피해를 적용
/// </summary>
public class AssasinAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    float dmg = 1f;
    [SerializeField]
    float changeDmg = 5f;
    Enemy enemy;

    Animator animator;
    float aLevel;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 기본 공격력에 레벨당 증가량을 더해 최종 피해량을 계산
    /// 실제 공격이 적중하는 시점에 호출
    /// </summary>
    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    /// <summary>
    /// 암살자의 공격 대상과 현재 레벨을 저장하고 공격 애니메이션을 시작
    /// 타워의 공격 주기가 되었을 때 외부 공격 관리 스크립트에서 호출
    /// </summary>
    public void Execute(Enemy target, float rnage, float level)
    {
        
        enemy = target;
        aLevel = level;
        animator.SetTrigger("isAttack");
    }

    /// <summary>
    /// 저장된 적에게 계산된 피해량을 직접 적용
    /// 공격이 적중하는 애니메이션 프레임의 이벤트를 통해 호출
    /// </summary>
    public void Dealing()
    {
        if (enemy == null) return;

        enemy.Damage(GetDamage(aLevel));
    }

}
