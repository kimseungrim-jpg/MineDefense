using UnityEngine;

/// <summary>
/// 궁수 타워의 공격 실행과 화살 생성을 담당
/// 공격 명령을 받으면 애니메이션을 재생
/// 애니메이션 이벤트가 호출되는 타이밍에 대상 적을 추적하는 화살을 생성
/// </summary>
public class AcherAttack : MonoBehaviour, IAttack
{
    public GameObject arrowPrefab;
    public Transform arrowPos;

    [SerializeField]float dmg = 5f;
    [SerializeField]float changeDmg = 10f;

    float aLevel;

    Animator animator;
    Enemy enemy;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 기본 공격력에 레벨당 공격력 증가량을 더해 최종 피해량을 계산
    /// 화살이 생성될 때 전달할 피해량을 구하기 위해 호출
    /// </summary>
    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    /// <summary>
    /// 궁수 타워의 공격을 시작
    /// 타워의 공격 주기가 되었을 때 외부 공격 관리 스크립트에서 호출
    /// 실제 화살 생성은 애니메이션 이벤트가 Dealing을 호출할 때 처리
    /// </summary>
    public void Execute(Enemy target, float rnage, float level)
    {
        enemy = target;
        aLevel = level;
        animator.SetTrigger("isAttack");
    }

    /// <summary>
    /// 저장된 적을 추적하는 화살을 생성하고 피해량을 전달
    /// 활을 발사하는 애니메이션 프레임의 이벤트를 통해 호출
    /// </summary>
    public void Dealing()
    {
        if (enemy == null) return;

        GameObject arrow = Instantiate(arrowPrefab, arrowPos.position, Quaternion.identity);

        arrow.GetComponent<ShotAttack>().SetTarget(enemy, GetDamage(aLevel));
    }
}
