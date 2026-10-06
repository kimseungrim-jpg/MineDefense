using UnityEngine;

/// <summary>
/// 타워의 공격 주기, 공격 대상 탐색, 강화 및 선택 UI 연결을 담당
/// 공격 방식 자체는 IAttack 구현체에 위임하여 타워 종류마다 다른 공격을 실행
/// </summary>
public class Tower : MonoBehaviour
{
    public string towerName = "전사";
    public float attackRange = 3f;
    public float attackRate = 1f;

    public float level = 1;
    public int upgradeCost = 3;
    

    private float attackTimer;
    private Enemy target;
    IAttack attack;

    [SerializeField]
    private GameObject selectCircle;

    public int buildCost = 50;

    public int sellCost = 30;

    /// <summary>
    /// 타워 오브젝트가 생성될 때, 생성된 해당 타워 GameObject에 붙어 있는 공격 구현체를 찾아 저장
    /// 이후 능력치 표시와 실제 공격 실행을 공통 인터페이스로 처리하기 위해 호출
    /// </summary>
    private void Awake()
    {
        attack = GetComponent<IAttack>();
    }

    /// <summary>
    /// 현재 레벨을 반영한 공격력과 타워의 주요 능력치를 UI 표시용 문자열로 반환
    /// 타워 정보 UI가 선택된 타워의 정보를 표시할 때 호출
    /// </summary>
    public string GetStats()
    {
        float currentDamage = attack.GetDamage(level);

        return $"레벨: {level}\n공격력: {currentDamage}\n공격 범위: {attackRange}\n강화 비용: {upgradeCost}ore\n판매가: {sellCost}G";
    }

    /// <summary>
    /// 매 프레임 공격 대상을 유지하거나 새로 탐색하고, 공격 주기가 되면 공격을 실행
    /// 현재 대상이 사라지거나 사거리 밖으로 벗어난 경우 다음 프레임부터 새로운 대상을 탐색
    /// </summary>
    private void Update()
    {
        attackTimer -= Time.deltaTime;

        // 대상이 없을 때만 새로운 적을 탐색하므로, 선택한 적은 제거되거나 사거리를 벗어날 때까지만 유지
        if (target == null)
        {
            target = FindTarget();
        }
        else
        {
            float dist = Vector2.Distance(
                transform.position,
                target.transform.position
            );

            if (dist > attackRange)
            {
                target = null;
            }
        }

        if (target != null && attackTimer <= 0f)
        {
            // 타워 종류별 공격 방식은 IAttack 구현체가 결정하며 필요한 대상, 범위, 레벨을 함께 전달
            attack.Execute(target, attackRange, level);

            attackTimer = attackRate;

        }
    }

    /// <summary>
    /// 보유 광석을 소비하여 타워 레벨을 올리고 다음 강화 비용을 증가시키는 함수
    /// 타워 정보 UI에서 강화가 확정되었을 때 호출
    /// </summary>
    public void Upgrade()
    {
        if (OreManager.instance.ore < upgradeCost)
        {
            Debug.Log($"광석이 부족합니다.{upgradeCost}");
            return;
        }

        OreManager.instance.ore -= upgradeCost;

        level++;
        upgradeCost += 3;

        EffectManager.instance.PlayerLevelupEffect(transform.position);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.SfxType.LevelUp);
        }

        Debug.Log($"타워 레벨{level}");
    }

    /// <summary>
    /// 타워의 원형 공격 범위 안에 있는 적 중 가장 가까운 적을 찾아 반환하는 함수
    /// 현재 공격 대상이 없을 때 Update에서 호출
    /// </summary>
    Enemy FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Enemy closeEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
            //공격 범위에 함께 검출된 지형이나 건설 슬롯 등은 대상 후보에서 제외
            if (!hit.CompareTag("Enemy"))
            {
                continue;
            }
            float dist = Vector2.Distance(transform.position, hit.transform.position);

            if (dist < minDistance)
            {
                minDistance = dist;
                closeEnemy = hit.GetComponent<Enemy>();
            }
        }

        return closeEnemy;
    }

    /// <summary>
    /// 타워의 Collider2D를 클릭했을 때 해당 타워의 정보 UI를 표시하고 선택 상태로 전환
    /// UI 위에서 발생한 클릭은 버튼 조작과 타워 선택이 동시에 처리되지 않도록 차단
    /// </summary>
    private void OnMouseDown()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        BuildSlot slot = GetComponentInParent<BuildSlot>();

        TowerInfoUI.instance?.Show(this, slot);
        MinerUpgradeUI.Instance?.Hide();

        if (TowerSelectManager.instance != null)
        {
            TowerSelectManager.instance.SelectedTower(this);
        }

    }

    /// <summary>
    /// 타워의 선택 표시 오브젝트를 활성화하거나 비활성화하는 함수
    /// TowerSelectManager가 선택 타워를 변경하거나 선택을 해제할 때 호출
    /// </summary>
    /// <param name="isVisible"></param>
    public void SetSelectTowerVisible(bool isVisible)
    {
        if (selectCircle != null)
        {
            selectCircle.SetActive(isVisible);
        }
    }

    /// <summary>
    /// Scene 뷰에서 타워가 선택되었을 때 실제 공격 범위를 원형으로 표시하기 위한 함수
    /// 공격 범위 설정과 타겟 탐색 범위를 확인할 때 사용
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
