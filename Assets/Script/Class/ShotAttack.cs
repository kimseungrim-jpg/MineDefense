using UnityEngine;

/// <summary>
/// 궁수 타워가 발사한 화살의 이동과 충돌 피해를 담당
/// 지정된 적을 매 프레임 추적, 적과 충돌하면 전달받은 피해를 적용하고 제거
/// </summary>
public class ShotAttack : MonoBehaviour
{
    public float Speed = 5f;

    private Enemy target;
    private float damage;

    /// <summary>
    /// 화살이 추적할 대상과 적중 시 적용할 피해량을 설정
    /// AcherAttack이 화살 프리팹을 생성한 직후 호출
    /// </summary>
    public void SetTarget(Enemy enemy, float dmg)
    {
        target = enemy;
        damage = dmg;
    }

    /// <summary>
    /// 매 프레임 대상 방향으로 화살을 이동시키고 진행 방향에 맞춰 회전
    /// 대상이 먼저 제거되었다면 더 이상 추적할 수 없으므로 화살도 제거
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

        // 투사체의 오른쪽 방향이 이동 방향을 바라보도록 z축 회전값을 계산
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /// <summary>
    /// 화살의 Trigger Collider가 다른 Collider와 접촉했을 때 호출
    /// Enemy 태그를 가진 대상에게 피해를 적용한 뒤 화살을 제거
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
