using UnityEngine;

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

    private void Awake()
    {
        attack = GetComponent<IAttack>();
    }

    public string GetStats()
    {
        float currentDamage = attack.GetDamage(level);

        return $"레벨: {level}\n공격력: {currentDamage}\n공격 범위: {attackRange}\n강화 비용: {upgradeCost}ore\n판매가: {sellCost}G";
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

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
            attack.Execute(target, attackRange, level);

            attackTimer = attackRate;

        }
    }

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

        Debug.Log($"타워 레벨{level}");
    }

    Enemy FindTarget()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange);

        Enemy closeEnemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var hit in hits)
        {
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

    private void OnMouseDown()
    {
        Debug.Log(
        $"[Tower 클릭 진입] {name} / " +
        $"Collider 활성화: {GetComponent<Collider2D>()?.enabled} / " +
        $"UI 위: {UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()}"
        );

        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log($"[Tower 클릭 차단] {name} - UI 위로 판정됨");
            return;
        }

        // 1. UI 클릭 방지 (버튼 누를 때 타워가 선택되는 것 방지)
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) return;

        // 2. 삭제 모드일 때의 동작
        if (BuildManager.instance.CurrentMode == BuildMode.Remove)
        {
            // 내 부모인 BuildSlot을 찾아 삭제 실행
            BuildSlot slot = GetComponentInParent<BuildSlot>();
            if (slot != null)
            {
                slot.RemoveTower();
            }
            return; // 삭제했으므로 여기서 함수 종료
        }
        else
        {
            BuildSlot slot = GetComponentInParent<BuildSlot>();
            TowerInfoUI.instance.Show(this, slot);
        }

            MinerUpgradeUI.Instance?.Hide();

        // 3. 일반 모드(또는 건설 모드가 아닐 때)의 동작
        // 업그레이드를 위한 선택 화살표 로직
        if (TowerSelectManager.instance != null)
        {
            TowerSelectManager.instance.SelectedTower(this);
        }

    }

    public void SetSelectTowerVisible(bool isVisible)
    {
        if (selectCircle != null)
        {
            selectCircle.SetActive(isVisible);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
