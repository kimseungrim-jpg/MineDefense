using UnityEngine;

/// <summary>
/// 전사 타워의 원형 범위 근점 공격을 담당
/// 공격 애니메이션을 실행한 뒤 애니메이션 이벤트 시점에 주변 적을 검색하여 피해를 적용
/// </summary>
public class WarriorAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    float dmg = 10f;
    [SerializeField]
    float changeDmg = 10f;

    Animator ani;
    float attackRange;
    float wLevel;

    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 기본 공격력에 레벨당 증가량을 더해 최종 피해량을 계산
    /// 범위 안에 있는 적에게 실제 피해를 적용할 때 호출
    /// </summary>
    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    /// <summary>
    /// 이번 공격에 사용할 범위와 타워 레벨을 저장하고 공격 애니메이션을 시작
    /// 타워의 공격 주기가 되었을 때 외부 공격 관리 스크립트에서 호출
    /// 전사는 대상 한 명이 아니라 공격 범위 안의 모든 적에게 피해를 적용
    /// </summary>
    public void Execute(Enemy target, float range, float level)
    {
        attackRange = range;
        wLevel = level;
        ani.SetTrigger("isAttack");
    }

    /// <summary>
    /// 전사 주변의 원형 범위를 검사하고 범위 안에 있는 모든 적에게 피해를 적용
    /// 무기가 적중하는 애니메이션 프레임의 이벤트를 통해 호출
    /// </summary>
    public void Dealing()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<Enemy>().Damage(GetDamage(wLevel));
            }
        }
    }
}
