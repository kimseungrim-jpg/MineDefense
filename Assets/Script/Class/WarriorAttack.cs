using Unity.VisualScripting;
using UnityEngine;

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

    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    public void Execute(Enemy target, float range, float level)
    {
        attackRange = range;
        wLevel = level;
        ani.SetTrigger("isAttack");
    }

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
