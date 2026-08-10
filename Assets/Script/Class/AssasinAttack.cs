using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AssasinAttack : MonoBehaviour, IAttack
{
    [SerializeField]
    float dmg = 1f;
    [SerializeField]
    float changeDmg = 5f;
    Enemy enemy;

    Animator animator;
    float aLevel;

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

        enemy.Damage(GetDamage(aLevel));
    }

}
