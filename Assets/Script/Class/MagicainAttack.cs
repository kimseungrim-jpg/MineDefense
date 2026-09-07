using UnityEngine;

/// <summary>
/// 마법사 타워의 공격 실행과 범위 공격 투사체 생성을 담당
/// 공격 애니메이션을 실행한 뒤 애니메이션 이벤트 시점에 폭발형 투사체를 생성
/// </summary>
public class MagicainAttack : MonoBehaviour, IAttack
{
    public GameObject magicPrefab;
    public Transform magicPos;

    [SerializeField]
    float dmg = 10;
    [SerializeField]
    float changeDmg = 10f;

    Enemy enemy;
    Animator animator;
    float mLevel;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 기본 공격력에 레벨당 증가량을 더해 최종 피해량을 계산
    /// 마법 투사체를 생성할 때 전달할 피해량을 구하기 위해 호출
    /// </summary>
    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    /// <summary>
    /// 마법사의 공격 대상과 현재 레벨을 저장하고 공격 애니메이션을 시작
    /// 타워의 공격 주기가 되었을 때 외부 공격 관리 스크립트에서 호출
    /// </summary>
    public void Execute(Enemy target, float range, float level)
    {
        enemy = target;
        mLevel = level;
        animator.SetTrigger("isAttack");
    }

    /// <summary>
    /// 지정된 적을 추적하는 마법 투사체를 생성하고 피해량을 전달
    /// 마법을 발사하는 애니메이션 프레임의 이벤트를 통해 호출
    /// </summary>
    public void Dealing()
    {
        if (enemy == null) return;

        GameObject magic = Instantiate(magicPrefab, magicPos.position, Quaternion.identity);

        magic.GetComponent<ShotRange>().SetTarget(enemy, GetDamage(mLevel));
    }
}
