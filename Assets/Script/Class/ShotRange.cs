using UnityEngine;

/// <summary>
/// 마법사 타워가 발사한 투사체의 이동과 범위 피해를 담당
/// 지정된 적을 추적하고 적과 충돌하면 주변의 모든 적에게 피해를 적용
/// </summary>
public class ShotRange : MonoBehaviour
{
    public float Speed = 5f;

    private Enemy target;
    private float damage;

    [SerializeField]
    float explosionRange = 1f;

    /// <summary>
    /// 투사체가 추적할 대상과 폭발 시 적용할 피해량을 설정
    /// MagicainAttack이 마법 투사체를 생성한 직후 호출
    /// </summary>
    public void SetTarget(Enemy enemy, float dmg)
    {
        target = enemy;
        damage = dmg;
    }

    /// <summary>
    /// 매 프레임 대상 방향으로 투사체를 이동시키고 진행 방향에 맞춰 회전
    /// 대상이 먼저 제거되었다면 더 이상 추적할 수 없으므로 투사체도 제거
    /// </summary>
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = (target.transform.position - transform.position).normalized;
        transform.position += dir * Speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// 투사체의 Trigger Collider가 다른 Collider와 접촉했을 때 호출
    /// Enemy 태그를 가진 대상과 충돌한 경우에만 범위 폭발을 실행
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        Explosion();

    }

    /// <summary>
    /// 현재 투사체 위치를 중심으로 원형 범위를 검사
    /// 범위 안에 있는 모든 적에게 동일한 피해를 적용한 뒤 투사체 제거
    /// </summary>
    void Explosion()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRange);

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Enemy")) continue;

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) continue;

            enemy.Damage(damage);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Scene 뷰에서 투사체가 선택되었을 때 실제 폭발 범위를 표시
    /// 공격 범위를 조정하거나 충돌 판정을 확인할 때 사용
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
