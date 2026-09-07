using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 적의 체력, 피격 연출, 사망 보상 및 웨이브 사망 통보를 담당
/// 이동은 EnemyMovement가 처리하며, 이 컴포넌트는 적의 전투 상태를 관리
/// </summary>
public class Enemy : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;

    public int goldReward = 10;
    private float hitFlashTime = 0.3f;

    private bool isDead = false;

    SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// 웨이브 난이도 배율을 최대 체력에 적용
    /// EnemySpawner가 적을 생성한 직후 호출
    /// </summary>
    public void HpUp(float Up)
    {
        maxHp = maxHp * Up;
    }

    /// <summary>
    /// 외부 공격에서 전달된 피해를 현재 체력에 적용하고 피격 연출을 실행
    /// 체력이 모두 소진되면 보상을 지급한 뒤 적을 사망 처리
    /// </summary>
    public void Damage(float damge)
    {
        if (isDead) return;

        currentHp -= damge;
        //Debug.Log(currentHp);

        StopAllCoroutines();
        StartCoroutine(HitFlash());

        if (currentHp <= 0)
        {
            isDead = true;
            GoldManager.instance.AddGold(goldReward);
            Die();
        }
    }

    /// <summary>
    /// 피격 직후 잠시 붉은색으로 표시
    /// 피격 연출을 위한 코루틴
    /// </summary>
    IEnumerator HitFlash()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(hitFlashTime);
        sprite.color = Color.white;
    }

    /// <summary>
    /// WaveManager에 적 제거 사실을 알리고 현재 적 오브젝트를 제거
    /// 체력이 소진되거나 EnemyMoverment가 목적지에 도착했을 때 호출
    /// </summary>
    public void Die()
    {
        WaveManager.instance.OnEnemyDead();
        Destroy(gameObject);
    }
}
