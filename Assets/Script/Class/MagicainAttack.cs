using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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

    public float GetDamage(float level)
    {
        return dmg + (level * changeDmg);
    }

    public void Execute(Enemy target, float range, float level)
    {
        enemy = target;
        mLevel = level;
        animator.SetTrigger("isAttack");
    }

    public void Dealing()
    {
        if (enemy == null) return;

        GameObject magic = Instantiate(magicPrefab, magicPos.position, Quaternion.identity);

        magic.GetComponent<ShotRange>().SetTarget(enemy, GetDamage(mLevel));
    }
}
