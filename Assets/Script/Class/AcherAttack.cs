using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AcherAttack : MonoBehaviour, IAttack
{
    public GameObject arrowPrefab;
    public Transform arrowPos;

    [SerializeField]
    float dmg = 5f;
    [SerializeField]
    float changeDmg = 10f;

    float aLevel;

    Animator animator;
    Enemy enemy;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    public void Execute(Enemy target, float rnage, float level)
    {
        enemy = target;
        aLevel = level;
        animator.SetTrigger("isAttack");
    }

    public void Dealing()
    {
        if (enemy == null) return;

        GameObject arrow = Instantiate(arrowPrefab, arrowPos.position, Quaternion.identity);

        arrow.GetComponent<ShotAttack>().SetTarget(enemy, GetDamage(aLevel));
    }
}
